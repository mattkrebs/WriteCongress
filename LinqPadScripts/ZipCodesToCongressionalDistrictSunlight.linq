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

//this will take atleast 15-20 (probably closer to 25-30) minutes but no biggy, should only be done once or twice a year

System.Net.WebClient wc = new System.Net.WebClient();
//info on the file can be seen here: http://sunlightlabs.github.com/congress/#bulk-data/zip-codes-to-congressional-districts
//arguably even better than house.gov in some ways
var districts = wc.DownloadString("http://assets.sunlightfoundation.com/data/districts.csv");
var count = 1;
foreach(var line in districts.Split(new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries)){
	var parts = line.Split(',');	
	var district = new ZipCodeCongressionalDistricts();
	district.PostalCode = parts[0];
	district.State = parts[1];
	district.District = int.Parse(parts[2]);	
	
	ZipCodeCongressionalDistricts.InsertOnSubmit(district);
	count++;
	if((count%100)==0){
		"saving".Dump();
		SubmitChanges();
	}
}
SubmitChanges();
"done".Dump();