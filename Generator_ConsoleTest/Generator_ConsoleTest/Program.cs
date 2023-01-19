using System;
using System.IO;
// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main()
    {

        // 입력값
        double draft = 16;
        double draftTe = draft + 4 + 0.59;
        double columnSpacingLon = 63.851;
        double columnSpacingTrans = 73.729;
        double columnLength = 12.75;
        double columnWidth = 15.588;
        double columnHeight = 29;
        double pontoonLength = 60;
        double pontoonWidth = 9;
        double pontoonHeight = 4.5;
        double braceLength = 60;
        double braceWidth = 1.5;
        double braceHeight = 3;

        // 추가 변수 값 
        double dl = 0.75;  //Default Length dl  
        double dl2 = 0.6;  //Default Length dl  
        double ml = columnWidth - ((pontoonWidth * 1.732) - (dl * 4));  // Middle Part Length ml

        //--------------------------------------------
        string direction = "";
        string composition = "";
        string type = "";
        int numbering = 0;
        //--------------------------------------------

        string script = "";
        /*
         * script += "GenieRules.Compatibility.version = \"V8.3-31\";\n";
        script += @"GenieRules.Tolerances.useTolerantModelling = true;
        GenieRules.Tolerances.angleTolerance = 2 deg;

        GenieRules.Meshing.autoSimplifyTopology = true;
        GenieRules.Meshing.eliminateInternalEdges = true;
        GenieRules.BeamCreation.DefaultCurveOffset = ReparameterizedBeamCurveOffset();
        GenieRules.Transformation.DefaultConnectedCopy = false;
        ";*/
        script += "\n// Create All Vector\n";
        script += "pi = math.pi();\n";
        for (int i = 30; i < 360; i += 30)
        {
            script += $"cos{i:000}p = math.cos(pi / 180 * {i});\n";
            script += $"sin{i:000}p = math.sin(pi / 180 * {i});\n";
            script += $"Vec_{i:000} = Vector3d(cos{i:000}p, sin{i:000}p, 0);\n";
        }

        script += "\n// Create Sections \n\n";
        // 소재 정의sdd
        script += $"St52 = MaterialLinear(335000000, 7850 kg/m^3, 2.1e+11 Pa, 0.3, 1.2e-05 delC^-1, 0.03 N*s/m);\n";
        script += $"Girder = UnsymISection(0.8, 0.04, 0.04, 0.02 m, 0.04, 0.5, 0.25 m, 0.05);\n";
        script += $"FlatBar_frm = BarSection(0.01, 0.07);\n";
        script += $"Stiffener = LSection(0.6, 0.04, 0.3, 0.04);\n";
        script += $"Stiffener.width = 0.3;\n";
        script += $"Stiffener.webThickness = 0.04;\n";
        script += $"Stiffener.setDefault();\n";
        script += $"Rail = PipeSection(0.2, 0.1);\n";
        script += $"Th09 = Thickness(0.009);\n";
        script += $"Th10 = Thickness(0.010);\n";
        script += $"Th11 = Thickness(0.011);\n";
        script += $"Th12 = Thickness(0.012);\n";
        script += $"Th13 = Thickness(0.013);\n";
        script += $"Th15 = Thickness(0.015);\n";
        script += $"Th20 = Thickness(0.020);\n";
        script += $"Th08 = Thickness(0.008);\n";
        script += $"St52.setDefault();\n";
        script += $"Th08.setDefault();\n";

        script += CreateNode.NDLine(dl, dl2, ml, pontoonWidth, columnLength, columnWidth);

        CreatePontoon createPontoon = new CreatePontoon();

        script += createPontoon.PTBody(dl, ml, pontoonWidth, pontoonHeight, pontoonLength);
        //script += CreatePontoon.PTFrame(dl, ml, pontoonWidth, pontoonHeight, pontoonLength);


        Console.WriteLine(script);

    }
}
public static class CreateNode
{
    public static string script = "";
    public static int ND_EE_BTM_B_P_num = 1;
    public static int ND_EE_BTM_B_C_num = 1;
    public static int ND_EE_BTM_A_P_num = 1;
    public static int ND_EE_BTM_A_C_num = 1;
    public static string refND_EE_BTM_B_P = "";
    public static string refND_EE_BTM_B_C = "";
    // 외곽 + 고정 선 그리기
    public static string NDLine(
            double dl, double dl2, double ml
            , double pontoonWidth, double columnLength, double columnWidth)
    {
        script = "";

        //NO_EE_BTM_B_P 점 
        script += $"ori = Point( 0, 0, 0 );\n";
        script += $"ND_EE_BTM_B_P_001 = Point(0, {ml} / 2, 0);\n";
        script += $"ND_EE_BTM_B_P_002 = ND_EE_BTM_B_P_001.copyTranslate(Vec_060.normalise() * ({pontoonWidth}));\n";
        script += $"ND_EE_BTM_B_P_003 = ND_EE_BTM_B_P_002.copyTranslate(Vec_330.normalise() * ({dl} * 3));\n";
        script += $"ND_EE_BTM_B_P_004 = ND_EE_BTM_B_P_002.copyTranslate(Vec_330.normalise() * ({dl} * 4));\n";
        script += $"ND_EE_BTM_B_P_005 = Point(ND_EE_BTM_B_P_004.x, {ml} / 2, 0);\n";

        //ND_EE_BTM_B_C_00
        // - FOR
        script += $"ND_EE_BTM_B_C_001 = CreateLineTwoPoints(ori, ND_EE_BTM_B_P_001);\n";
        script += $"ND_EE_BTM_B_C_002 = CreateLineTwoPoints(ND_EE_BTM_B_P_001, ND_EE_BTM_B_P_002);\n";
        script += $"ND_EE_BTM_B_C_003 = CreateLineTwoPoints(ND_EE_BTM_B_P_002, ND_EE_BTM_B_P_003);\n";
        script += $"ND_EE_BTM_B_C_004 = CreateLineTwoPoints(ND_EE_BTM_B_P_003, ND_EE_BTM_B_P_004);\n";
        script += $"ND_EE_BTM_B_C_005 = CreateLineTwoPoints(ND_EE_BTM_B_P_004, ND_EE_BTM_B_P_005);\n";
        script += $"ND_EE_BTM_B_C_006 = CreateLineTwoPoints(ND_EE_BTM_B_P_005, ND_EE_BTM_B_P_001);\n";

        // Vec_240 방향 선 긋기 - intersect - divide_YPlane3d - delete, delete
        script += $"dummy_p = ND_EE_BTM_B_P_003.copyTranslate(Vec_240.normalise() * ({pontoonWidth}));\n";
        script += $"ND_EE_BTM_B_C_007 = CreateLineTwoPoints(ND_EE_BTM_B_P_003, dummy_p);\n";
        script += $"ND_EE_BTM_B_P_006 = ND_EE_BTM_B_C_007.intersect(ND_EE_BTM_B_C_006);\n";
        script += $"dummy_c = ND_EE_BTM_B_C_007.divide(YPlane3d(ND_EE_BTM_B_P_006.y));\n";
        script += $"Delete(dummy_c);\n";
        script += $"Delete(dummy_p);\n";

        script += $"ND_EE_BTM_B_P_007 = ND_EE_BTM_B_P_003.copyTranslate(Vec_240.normalise() * ({dl} * 4));\n";
        script += $"dummy_p = ND_EE_BTM_B_P_007.copyTranslate(Vec_330.normalise() * ({dl} * 4));\n";
        script += $"ND_EE_BTM_B_C_008 = CreateLineTwoPoints(ND_EE_BTM_B_P_007, dummy_p);\n";
        script += $"ND_EE_BTM_B_P_008 = ND_EE_BTM_B_C_008.intersect(ND_EE_BTM_B_C_005);\n";
        script += $"dummy_c = ND_EE_BTM_B_C_008.divide(XPlane3d(ND_EE_BTM_B_P_008.x));\n";
        script += $"Delete(dummy_p);\n";
        script += $"Delete(dummy_c);\n";
        script += $"\n";

        //// 외곽, 점 -선
        script += $"ND_EE_BTM_A_P_001 = ND_EE_BTM_B_P_002.copyTranslate(Vec_330.normalise() * ({columnLength}));\n";
        script += $"ND_EE_BTM_A_P_002 = Point(ND_EE_BTM_A_P_001.x, {ml} / 2, 0);\n";
        script += $"ND_EE_BTM_A_P_003 = ND_EE_BTM_A_P_001.copyTranslate(Vec_150.normalise() * ({dl} * 3));\n";
        script += $"ND_EE_BTM_A_P_004 = Point(ND_EE_BTM_A_P_003.x, {ml} / 2, 0);\n";
        script += $"\n";
        script += $"ND_EE_BTM_A_C_001 = CreateLineTwoPoints(ND_EE_BTM_B_P_004, ND_EE_BTM_A_P_001);\n";
        script += $"ND_EE_BTM_A_C_002 = CreateLineTwoPoints(ND_EE_BTM_A_P_001, ND_EE_BTM_A_P_002);\n";
        script += $"ND_EE_BTM_A_C_003 = CreateLineTwoPoints(ND_EE_BTM_A_P_002, ND_EE_BTM_B_P_005);\n";
        script += $"ND_EE_BTM_A_C_004 = CreateLineTwoPoints(ND_EE_BTM_A_P_003, ND_EE_BTM_A_P_004);\n";
        script += $"\n";
        script += $"dummy_p = ND_EE_BTM_B_P_008.copyTranslate(Vec_330.normalise() * ({columnLength}));\n";
        script += $"ND_EE_BTM_A_C_005 = CreateLineTwoPoints(ND_EE_BTM_B_P_008, dummy_p);\n";
        script += $"ND_EE_BTM_A_P_005 = ND_EE_BTM_A_C_005.intersect(ND_EE_BTM_A_C_003);\n";
        script += $"dummy_c = ND_EE_BTM_A_C_005.divide(YPlane3d(ND_EE_BTM_A_P_005.y));\n";
        script += $"Delete(dummy_p);\n";
        script += $"Delete(dummy_c);\n";
        script += $"\n";
        script += $"ND_EE_BTM_A_P_006 = ND_EE_BTM_B_P_004.copyTranslate(Vec_330.normalise() * ({dl} * 4));\n";
        script += $"ND_EE_BTM_A_P_007 = ND_EE_BTM_A_P_006.copyTranslate(Vec_240.normalise() * ({dl} * 4));\n";
        script += $"ND_EE_BTM_A_C_006 = CreateLineTwoPoints(ND_EE_BTM_A_P_006, ND_EE_BTM_A_P_007);\n";
        script += $"\n";
        script += $"ND_EE_BTM_C_P_001 = Point(ND_EE_BTM_A_P_001.x, 0, 0);\n";
        script += $"ND_EE_BTM_C_P_002 = Point(ND_EE_BTM_A_P_003.x, 0, 0);\n";
        script += $"ND_EE_BTM_C_P_003 = Point(ND_EE_BTM_B_P_004.x, 0, 0);\n";
        script += $"ND_EE_BTM_C_C_001 = CreateLineTwoPoints(ori, ND_EE_BTM_C_P_001);\n";
        script += $"ND_EE_BTM_C_C_002 = CreateLineTwoPoints(ND_EE_BTM_C_P_001, ND_EE_BTM_A_P_002);\n";
        script += $"ND_EE_BTM_C_C_003 = CreateLineTwoPoints(ND_EE_BTM_C_P_002, ND_EE_BTM_A_P_003);\n";
        script += $"ND_EE_BTM_C_C_004 = CreateLineTwoPoints(ND_EE_BTM_C_P_003, ND_EE_BTM_B_P_005);\n";

        // ND_EE_BTM_B 폰툰 쪽 선 채우기
        ND_EE_BTM_B_C_num = 8;
        // 채울 때 반복된 수 
        int temp = (int)(Math.Truncate(pontoonWidth / dl)) - 2;
        for (int i = 1; i <= temp; i++)
        {
            ND_EE_BTM_B_C_num++;
            script += $"ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000} = ND_EE_BTM_B_C_003.copyTranslate(Vec_240.normalise() * ({dl} *{i}));\n";
        }

        // 끄트머리 공간 선 긋기
        ND_EE_BTM_B_P_num = 8;
        ND_EE_BTM_B_P_num++;
        script += $"ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000} = Point({dl2} / 1.732 * 2, {ml} / 2, 0);\n";
        script += $"dummy_p = ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}.copyTranslate(Vec_060.normalise() * (2));\n";
        ND_EE_BTM_B_C_num++;
        script += $"ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000} = CreateLineTwoPoints(ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}, dummy_p);\n";
        ND_EE_BTM_B_P_num++;
        script += $"ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000} = ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000}.intersect(ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num - 1:000});\n";
        script += $"dummy_c = ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000}.divide(YPlane3d(ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}.y));\n";
        script += $"Delete(dummy_c);\n Delete(dummy_p);\n";

        ND_EE_BTM_B_P_num++;
        script += $"ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000} = ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num - 2:000}.copyTranslate(Vec_060.normalise() * ({dl2} * 1));\n";
        script += $"dummy_p = ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}.copyTranslate(Vec_330.normalise() * (2));\n";
        ND_EE_BTM_B_C_num++;
        script += $"ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000} = CreateLineTwoPoints(ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}, dummy_p);\n";
        script += $"dummy_c = ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000}.divide(YPlane3d({ml}/2));\n";
        script += $"delete(dummy_p);\n";
        script += $"delete(dummy_c);\n";


        if ((pontoonWidth - temp * dl - (dl2 / 1.732)) - dl2 * 2 > 0)
        {
            ND_EE_BTM_B_P_num++;
            script += $"ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000} = ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num - 1:000}.copyTranslate(Vec_060.normalise() * ({dl2} * 1));\n";
            script += $"dummy_p = ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}.copyTranslate(Vec_330.normalise() * (2));\n";
            ND_EE_BTM_B_C_num++;
            script += $"ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000} = CreateLineTwoPoints(ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}, dummy_p);\n";
            script += $"dummy_p2 = ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000}.intersect(ND_EE_BTM_B_C_007);\n";
            script += $"dummy_c = ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000}.divide(YPlane3d(dummy_p2.y));\n";
            script += $"delete(dummy_p);\n";
            script += $"delete(dummy_c);\n";
        }


        ND_EE_BTM_A_P_num = 7;
        ND_EE_BTM_A_C_num = 6;

        script += $"\n// \n";
        // 살 붙이기 -2   // intersect 로 b 점 만들기 
        for (int i = 1; i <= 4; i++)
        {
            ND_EE_BTM_A_P_num++;
            script += $"ND_EE_BTM_A_P_{ND_EE_BTM_A_P_num:000} = ND_EE_BTM_A_P_006.copyTranslate(Vec_240.normalise() * ({dl} * 4 / 5* {i}));\n";
            script += $"dummy_p = ND_EE_BTM_A_P_{ND_EE_BTM_A_P_num:000}.copyTranslate(Vec_150.normalise() * ({dl} * 5));\n";
            ND_EE_BTM_A_C_num++;
            script += $"ND_EE_BTM_A_C_{ND_EE_BTM_A_C_num:000} = CreateLineTwoPoints(ND_EE_BTM_A_P_{ND_EE_BTM_A_P_num:000} , dummy_p);\n";
            ND_EE_BTM_B_P_num++;
            script += $"ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000} = ND_EE_BTM_A_C_{ND_EE_BTM_A_C_num:000}.intersect(ND_EE_BTM_B_C_005);\n";
            script += $"dummy_c = ND_EE_BTM_A_C_{ND_EE_BTM_A_C_num:000}.divide(XPlane3d(ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num:000}.x));\n";
            script += $"delete(dummy_p);\n";
            script += $"delete(dummy_c);\n";
        }


        for (int i = 1; i <= 4; i++)
        {
            ND_EE_BTM_B_P_num++;
            script += $"dummy_p = ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num - 4:000}.copyTranslate(Vec_180.normalise()*({dl} *3));\n";
            ND_EE_BTM_B_C_num++;
            script += $"ND_EE_BTM_B_C_{ND_EE_BTM_B_C_num:000} = CreateLineTwoPoints(ND_EE_BTM_B_P_{ND_EE_BTM_B_P_num - 4:000} , dummy_p);\n";

            script += $" \n";

            //ND_EE_BTM_B_P_{ ND_EE_BTM_B_P_num: 000}
        }
        return script;
    }
}



public static class CreateFile
{
    public static void FileDotjs(string script, string fileName)
    {

        string filePath = "C:\\Users\\Desktop\\" + fileName + ".js"; // 주소 확인
        string answer = "";

        // Console.WriteLine(script);
        if (File.Exists(filePath))
        {
            Console.WriteLine("파일이 이미 존재합니다.");
        }
        else
        {
            Console.Write("파일을 생성하시겠습니까? (y/n) \n >>> ");
            answer = Console.ReadLine();
            if (answer == "y" || answer == "yes")
            {
                File.WriteAllText(filePath, script);
                Console.WriteLine("파일생성이 완료되었습니다.");
            }
            else
            {
                Console.WriteLine("파일을 생성하지 않았습니다.");
            }
        }

        // Compartment 실행 명령어 
        //cm = CompartmentManager();
    }
}


