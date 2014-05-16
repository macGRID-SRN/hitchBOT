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
  <Output>DataGrids</Output>
  <Reference Relative="..\hitchbotAPI\hitchbotAPI\bin\hitchbotAPI.dll">&lt;MyDocuments&gt;\GitHub\hitchBOT\server\hitchbotAPI\hitchbotAPI\bin\hitchbotAPI.dll</Reference>
</Query>

//This is some seed data which now that I think about it I could have put into the project itself, but this is more convienient because it is with LINQpad.
Projects.InsertOnSubmit(new Projects(){Name="TestProject", StartTime = DateTime.UtcNow, TimeAdded = DateTime.UtcNow, Description="Test Project Description"});
SubmitChanges();
HitchBOTs.InsertOnSubmit(new HitchBOTs(){Name = "HitchBot", CreationTime = DateTime.UtcNow, TimeAdded = DateTime.UtcNow, Project_ID = 1});
SubmitChanges();
Locations.InsertOnSubmit(new Locations(){NearestCity = "St. John's", Latitude = 47.5675, Longitude= -52.7072, HitchBOT_ID=1, Accuracy = 0.4f, Altitude= 0, Velocity=0, TakenTime = DateTime.UtcNow, TimeAdded = DateTime.UtcNow});
Locations.InsertOnSubmit(new Locations(){NearestCity = "Vancouver", Latitude = 49.2500, Longitude= -123.1000, HitchBOT_ID=1, Accuracy = 0.4f, Altitude= 0, Velocity=0, TakenTime = DateTime.UtcNow, TimeAdded = DateTime.UtcNow});

SubmitChanges();
