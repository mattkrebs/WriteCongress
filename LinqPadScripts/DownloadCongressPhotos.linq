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

System.Net.WebClient wc = new System.Net.WebClient();
foreach(var person in Persons.ToList()){
	string url = String.Format("http://www.govtrack.us/data/photos/{0}-50px.jpeg",person.OpenCongressId);
	try{
	System.IO.File.WriteAllBytes(String.Format("C:\\temp\\congressphotos\\{0}-50px.jpg",person.OpenCongressId),wc.DownloadData(url));
	}catch(System.Net.WebException we){
	url.Dump();
		continue;
	}
}
Console.WriteLine("done");