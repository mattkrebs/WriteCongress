<Query Kind="Statements">
  <Connection>
    <ID>feb7feeb-a87c-41b6-aaef-6ca6be93f8ca</ID>
    <Persist>true</Persist>
    <Server>ikgdngr4jq.database.windows.net</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>writecongress</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAArSz2syId0anvBbOl0elvAAAAAACAAAAAAAQZgAAAAEAACAAAAAxaIuHL+EROQEkuCxeKK9CasrDLSfoL9TX7unichdeDAAAAAAOgAAAAAIAACAAAACdy0B5/l0vRSkzI6pV5tEJe/oU0L8G1cigH2bstZrlGSAAAACYP9RE7Y/03UJ22zgIk0C1epR/LMus5AqFItOhk4AFeUAAAABoxQPXDjKX+nrtsWNFns7lmKb8xbgxicI5w3spk9y8CNvMvtzXb6Usey+x9jsLo6J10du3YL2AGwM233OwXUYT</Password>
    <DbVersion>Azure</DbVersion>
    <Database>WriteCongress</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//DONT USE THIS UNLESS YOU REALLY NEED TO. USE THE SUNGLIGHT VERSION. THIS ONE IS SUPER SUPER SUPER SUPER SLOW (10+ hours)



System.Net.WebClient wc = new System.Net.WebClient();
var congressionalDistricts =ZipCodeCongressionalDistricts.ToList();
var zipCodes = ZipCodes.ToList().OrderBy (o =>int.Parse(o.PostalCode)).ToList();
var districtsToSave = new List<ZipCodeCongressionalDistricts>();



//try skipping any requests we know are bad when re-running so not to hit house.gov too hard
var skipListPath = @"C:\\temp\skip.txt";
Dictionary<string,bool> skipList = new Dictionary<string,bool>();
if(System.IO.File.Exists(skipListPath)){
	var lines = System.IO.File.ReadAllLines(skipListPath).ToList();
	lines.ForEach((s)=>{
		if(!skipList.ContainsKey(s)){
			skipList.Add(s,false);
		}
	});
	File.Delete(skipListPath);
}
var sw = new StreamWriter(skipListPath,false);

for(var i=0;i<zipCodes.Count;i++){
	var zipcode = zipCodes[i];	
	if(congressionalDistricts.Any (d =>d.PostalCode==zipcode.PostalCode )){
		continue;
	}
	if(skipList.ContainsKey(zipcode.PostalCode)==true){
		continue;
	}
	
	string url = String.Format("http://www.house.gov/htbin/findrep?ZIP={0}",zipcode.PostalCode);	
	var page = wc.DownloadString(String.Format(url));	
	var startIndex = page.IndexOf("districts=[");
	
	if(startIndex>-1){
		var districts = page.Substring(startIndex,page.IndexOf("];",startIndex)-startIndex).Replace("districts=[",string.Empty).Replace("\"",string.Empty);
		foreach(var district in districts.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries)){
			var state = district.Substring(0,2);
			var number = int.Parse(district.Substring(2,2));
			var entry = new ZipCodeCongressionalDistricts();
			entry.District = number;
			entry.State = state;
			entry.PostalCode = zipcode.PostalCode;			
			districtsToSave.Add(entry);
		}		
		//Console.WriteLine("{0}: {1}",zipcode.PostalCode,districts);				
	}else{
	sw.WriteLine(zipcode.PostalCode);
	sw.Flush();
	}
	
	if(((districtsToSave.Count+1)%100)==0){
		Console.WriteLine("saving {0}....",districtsToSave.Count);
		ZipCodeCongressionalDistricts.InsertAllOnSubmit(districtsToSave);
		SubmitChanges();
		
		districtsToSave.Clear();
	}
}
sw.Close();
ZipCodeCongressionalDistricts.InsertAllOnSubmit(districtsToSave);
SubmitChanges();
districtsToSave.Clear();

