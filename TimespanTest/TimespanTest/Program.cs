// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

DateTime myBirthday = Convert.ToDateTime("1991-01-31");
TimeSpan myLifeDay = DateTime.Now - myBirthday;


Console.WriteLine($"내 출생년도는 {myBirthday.Year}년이고, 나는 지금까지 {myLifeDay.Days} 일을 살아왔다.");

Char a = '0';
if (Char.IsNumber(a)) Console.Write($"{a} is the Number");

string unique = Guid.NewGuid().ToString();
Console.WriteLine("This is the Guid string value that created right before : " + unique);