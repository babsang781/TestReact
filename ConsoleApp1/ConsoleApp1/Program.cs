// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
/*
string multiLines = @"
안녕하세요,
반갑습니다.
";
Console.WriteLine(multiLines);

string method = "hello";
method

    int number = 3;
string result = "홀수";
Console.WriteLine($"{number} 은 {result} 입니다.");

string massage = "이";
Console.WriteLine("1 {0} 3 ", massage);
Console.WriteLine("1 " + massage + " 3");
Console.WriteLine($"1 {massage} 3 ");

string name = "C#";
string version = ".net core";
Console.WriteLine($"{name} 언어의 버전은 {version} 이다.");*/

// input & output
Console.WriteLine("이름을 입력하세요>> ");t
string name = Console.ReadLine();
Console.WriteLine(" 입력 받은 이름은 : " + name);
int test = Console.Read();
Console.WriteLine(test);

string location = "ND";
string valueType = "P";
string compass = "EE";
string composition = "BTM_A";
int numbering = 001;
int num1 = 1;
for (int i = 0;i< test; i++)
{
    Console.WriteLine($"{location}_{ valueType}_{compass}_{composition}_{numbering} = point( 0,0,{num1});");
    numbering++;
    num1++;
}
