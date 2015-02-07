<Query Kind="Expression">
  <Connection>
    <ID>240c39c6-6723-4e5c-b411-d2d0a9249cc1</ID>
    <Persist>true</Persist>
    <Server>jayqa4lbl8.database.windows.net</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>hitchbot</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA3lxYDhCngU6C76s9xZ3AOwAAAAACAAAAAAAQZgAAAAEAACAAAAACok8tDrIG5jm7s59WPsfDyXkgnZQSB0zzgbefhIGmvgAAAAAOgAAAAAIAACAAAAADcDL84BHM+dWl5YkJWRtDW1ZnzFZBrwqP3S1BiklrixAAAADxjp+OLRHHinLk8bH86PSzQAAAAJNr5nTtlQYArQhZpMih6UKwrjtFD2Krvbk4HUKVzvUqCJOWTcRO00CbEKsdeae9OQsfRuDR2sbWeTv8NZw9VYM=</Password>
    <DbVersion>Azure</DbVersion>
    <Database>hitchbotAPI_db</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Output>DataGrids</Output>
</Query>

WikipediaEntries.Where(l=>l.HitchBot_ID == 10).Select(l=> new { Name = l.EntryName, WikiText = string.Join(" / ", l.WikipediaText.Split('\n'))})  