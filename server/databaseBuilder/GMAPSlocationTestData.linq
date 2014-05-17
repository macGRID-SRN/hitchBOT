<Query Kind="Statements">
  <Connection>
    <ID>240c39c6-6723-4e5c-b411-d2d0a9249cc1</ID>
    <Persist>true</Persist>
    <Server>jayqa4lbl8.database.windows.net</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>hitchbot</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA2foI7GIrIUqaECsygztqIwAAAAACAAAAAAAQZgAAAAEAACAAAAA4qwEDai1F0VRWL5pTFVTZo6MBF6rqvQ5S5OAw/+YDaQAAAAAOgAAAAAIAACAAAADjOkLbXok/eZnFF/I5kF88g0zhTg+Srq5PQGVAiPhNohAAAAC7he+ATmCasYvJ53DzlqJ4QAAAABMBSYYa9xw2ESWpnM76Ma8xrbnq9U9hvmqTJyBQAIZCy1NSvWJCN59pLjxdtIYJplHZP6qpgliaWxvI+Q97o2k=</Password>
    <DbVersion>Azure</DbVersion>
    <Database>hitchbotAPI_db</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

HitchBOTs.InsertOnSubmit(new HitchBOTs(){ Project_ID = 1, Name = "LocationTester", CreationTime = DateTime.UtcNow, TimeAdded = DateTime.UtcNow});
SubmitChanges();
Locations.InsertOnSubmit(new Locations(){ HitchBOT_ID = 2, Latitude = 38.5, Longitude = -120.2, TakenTime = DateTime.Parse("5/17/2014 13:05:05"), TimeAdded = DateTime.UtcNow});
Locations.InsertOnSubmit(new Locations(){ HitchBOT_ID = 2, Latitude = 40.7, Longitude = -120.95, TakenTime = DateTime.Parse("5/17/2014 13:06:05"), TimeAdded = DateTime.UtcNow});
Locations.InsertOnSubmit(new Locations(){ HitchBOT_ID = 2, Latitude = 43.252, Longitude = -126.453, TakenTime = DateTime.Parse("5/17/2014 13:07:05"), TimeAdded = DateTime.UtcNow});
SubmitChanges();