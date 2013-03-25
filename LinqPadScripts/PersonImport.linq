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

var wc = new System.Net.WebClient();
var xml = wc.DownloadString("http://www.govtrack.us/data/us/113/people.xml");

XDocument doc = XDocument.Parse(xml);
var persons = (from p in doc.Descendants("person")
				select new{
					Id=p.Attribute("id").Value,
					LastName = (string)p.Attribute("lastname"),					
					FirstName = (string)p.Attribute("firstname"),
					FullNameAndTitle= (string)p.Attribute("name"),
					BirthDate = (string)p.Attribute("birthday"),
					Title = (string)p.Attribute("title"),					
					State = (string)p.Attribute("state"),
					District = (string)p.Attribute("district"),					
					Address = (string)(p.Descendants("role").OrderByDescending (o => DateTime.Parse((string)o.Attribute("startdate"))).FirstOrDefault().Attribute("address")),
					Party = (string)(p.Descendants("role").OrderByDescending (o => DateTime.Parse((string)o.Attribute("startdate"))).FirstOrDefault().Attribute("party")),
				}).ToList();
				
				persons.Dump();
foreach(var person in persons){
	
	var p = new Person();
	p.OpenCongressId = person.Id;
	p.FirstName = person.FirstName;
	p.LastName = person.LastName;
	p.FullNameAndTitle = person.FullNameAndTitle;
	
	DateTime birthDate = DateTime.Now;
	if(DateTime.TryParse(person.BirthDate,out birthDate)){
		p.BirthDate = birthDate;
	}	
	
	p.Title = person.Title;
	p.State = person.State;
	
	int district = -1;
	if(int.TryParse(person.District,out district)){
		p.District = district;
	}
	p.Session = 113;
	
	p.Address = person.Address;
	p.Party = person.Party;
	Persons.InsertOnSubmit(p);
}
SubmitChanges();
Console.WriteLine("done");
				
				
				