<Query Kind="Statements" />

//jspaur pulled this from his TSUG libraries, but it should work anywhere
			
			var zipPath = Path.Combine(Path.GetTempPath(),"US.zip");
			var outputPath = Path.Combine(Path.GetTempPath(),"us.txt");							
			//if the file exists previously, nuke it
            if(File.Exists(zipPath)) {
                File.Delete(zipPath);				
            }						
			
            //download the file and save it in my running dir
            Console.WriteLine("downloading file....");
            new System.Net.WebClient().DownloadFile("http://download.geonames.org/export/zip/US.zip",zipPath);            
            
            Console.WriteLine("unzipping file....");

			//7za.exe is 7zip, you can get this at http://sourceforge.net/projects/sevenzip/files/7-Zip/9.20/7za920.zip/download?use_mirror=hivelocity
			
			ProcessStartInfo psi = new ProcessStartInfo(@"c:\tools\7za.exe","e "+zipPath);
			psi.WorkingDirectory = Path.GetTempPath();
			psi.Arguments.Dump();
			var process = System.Diagnostics.Process.Start(psi);
			process.WaitForExit();            
            
            //okay, load the file into a datatable, we're going to bulk copy this fucker
            string[] lines = File.ReadAllLines(outputPath);

            System.Data.DataTable dt = new System.Data.DataTable("ZipCodes");
            dt.Columns.Add("PostalCode",typeof(string));
            dt.Columns.Add("City",typeof(string));
            dt.Columns.Add("State",typeof(string));
            dt.Columns.Add("StateAbbreviation",typeof(string));
            dt.Columns.Add("County",typeof(string));
            dt.Columns.Add("AdminCode",typeof(string));
            dt.Columns.Add("Latitude",typeof(float));
            dt.Columns.Add("Longitude",typeof(float));

            Console.WriteLine("loading file....");
            dt.BeginLoadData();
            
            for(int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                string[] parts = line.Split('\t');

                var z = dt.NewRow();
                z[0] = parts[1];
                z[1] = parts[2];
                z[2] = parts[3];                
                z[3] = parts[4];
                z[4] = parts[5];
                z[5] = parts[6];
                z[6] = float.Parse(parts[9]);
                z[7] = float.Parse(parts[10]);

                dt.Rows.Add(z);
            }            

			var csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
			csb.DataSource = "ikgdngr4jq.database.windows.net";
			csb.UserID = "writecongress";
			csb.Password="m5@VWmj5oVFT#RWDyl";
			csb.InitialCatalog = "writecongress";
			
            string connectionString = csb.ToString();

            Console.WriteLine("starting bulk copy");            
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //delete the old data
            using(System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString)) {
                conn.Open();

                string truncate = "TRUNCATE TABLE ZipCodes";
                var cmd = new System.Data.SqlClient.SqlCommand(truncate,conn);
                cmd.ExecuteNonQuery();                
            }


            System.Data.SqlClient.SqlBulkCopy sbc = new System.Data.SqlClient.SqlBulkCopy(connectionString);            
            sbc.DestinationTableName = "ZipCodes";
            sbc.BulkCopyTimeout = 120; //2 mins
            sbc.WriteToServer(dt);
            Console.WriteLine("bulk copy finished");
            
            
            Console.WriteLine("generating geographic points for zipcodes based on lat/long");
            using(System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString)) {
                conn.Open();

                //this sets the point column to be a sql server type of point which allows us to do distance math really easily
                string sql = "UPDATE ZipCodes SET Point = geography::STPointFromText('POINT('+CAST(Longitude as varchar(25))+' '+CAST(Latitude as varchar(25))+')',4326)";               
                
                var cmd = new System.Data.SqlClient.SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();                

//                sql =  "INSERT INTO PopularCities(City,[State]) SELECT DISTINCT City,[State] FROM TopCities";
//                cmd = new System.Data.SqlClient.SqlCommand(sql,conn);
//                cmd.ExecuteNonQuery();
            }            
            
            sw.Stop();
            
            Console.WriteLine("finished loading {1} zipcodes in {0} seconds. press any key to quit.",sw.Elapsed.TotalSeconds,lines.Length);