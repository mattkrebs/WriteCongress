<Query Kind="Statements">
  <Connection>
    <ID>feb7feeb-a87c-41b6-aaef-6ca6be93f8ca</ID>
    <Server>ikgdngr4jq.database.windows.net</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>writecongress</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAArFwFQYMXk0Wy7JDY8GzQGAAAAAACAAAAAAADZgAAwAAAABAAAABYFC5He/nD8zXEggiLcXNiAAAAAASAAACgAAAAEAAAAFrZeJVB2IH+9M6LcQb8M5QYAAAA4vmjzdL+MZGfFaVGRD6D0ApjjFq0EyZdFAAAAOUsF53w03hFAVwuBbh+uo2hOT+a</Password>
    <DbVersion>Azure</DbVersion>
    <Database>WriteCongress</Database>
    <ShowServer>true</ShowServer>
    <Persist>true</Persist>
  </Connection>
</Query>

var wc = new System.Net.WebClient();
var xml = wc.DownloadString("http://www.govtrack.us/api/v2/bill?congress=113&limit=10000&format=xml");

XDocument doc = XDocument.Parse(xml);
Console.WriteLine(doc.Descendants("item").Count().ToString());
var bills = (from p in doc.Descendants("item")
				where (string)p.Element("bill_resolution_type") == "bill"
				select new {
					BillType = (string)p.Element("bill_type"),
					Session = (int)p.Element("congress"),
					Status = (string)p.Element("current_status"),
					Number = (int)p.Element("number"),
					TitleCommon = (string)p.Element("title_without_number"),
					Title = (string)p.Element("title"),	
					StatusDate = DateTime.Parse((string)p.Element("current_status_date")),
					PermaLink = (string)p.Element("link"),
					DisplayNumber = (string)p.Element("display_number"),
					SponsorId = (int)p.Element("sponsor").Element("id"),
					Id = (int)p.Element("id")
				}).ToList();
		
	bills.Dump();
foreach(var bill in bills){
	
	var update = true;
	Bill b = Bills.Where(bi => bi.BillId == bill.Id).FirstOrDefault();
	if (b == null){
		update = false;
		b = new Bill();
	}
	b.Dump();
	b.BillId = bill.Id;
	b.Session = bill.Session;
	b.Number = bill.Number;
	b.SponsorId = bill.SponsorId;
	b.Title = bill.Title;
	b.TitleCommon = bill.TitleCommon;
	b.UpdatedDate = DateTime.Now;
	b.LastActionDate = bill.StatusDate;
	b.Status = bill.Status;
	b.TypeNumber = bill.DisplayNumber;
	b.PermaLink = bill.PermaLink;
	b.BillType = bill.BillType;

	if (!update){
	
	
		Bills.InsertOnSubmit(b);
		}
	
}
SubmitChanges();
Console.WriteLine("done");
