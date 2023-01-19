using System;

class CreatePontoon
{
    private string script = "";
    private int PT_NE_BTM_SF_num = 1;
    private int PT_NE_TOP_SF_num = 1;
    private int PT_NE_ELV_SF_num = 1;
    private int PT_NE_BHD_SF_num = 1;
    private string ref_PT_NE_BTM_SF_1 = "";
    private string ref_PT_NE_TOP_SF_1 = "";
    private string ref_PT_NE_ELV_SF_1 = "";
    private string ref_PT_NE_BHD_SF_1 = "";

    // frame 관련 스펙 작업 - 수정 가능 작업
    private double radiusOfFrm = 0.6;
    private double frmElvSpacing = 0.8;
    private double frmTopSpacing = 0.8;
    private double frmBtmSpacing = 0.9;
    private double pontoonBhdSpacing = 0.4;
    private double pontoonFrameSpacing = 2;

    public CreatePontoon()
    {
        script = "";
        PT_NE_BTM_SF_num = 1;
        PT_NE_TOP_SF_num = 1;
        PT_NE_ELV_SF_num = 1;
        PT_NE_BHD_SF_num = 1;
        ref_PT_NE_BTM_SF_1 = "";
        ref_PT_NE_TOP_SF_1 = "";
        ref_PT_NE_ELV_SF_1 = "";
        ref_PT_NE_BHD_SF_1 = "";

        // fr 관련 스펙 작업 - 수정 가능 작업
        iusOfFrm = 0.6;
        ElvSpacing = 0.8;
        frmTopSpacing = 0.8;
        frmBtmSpacing = 0.9;
        pontoonBhdSpacing = 0.4;
        pontoonFrameSpacing = 2;
}

    public string PTBody(double dl, double ml, double pontoonWidth, double pontoonHeight, double pontoonLength)
    {
        script += "\n// Create Pontoon Plate\n\n";
        //폰툰 끝 점
        script += $"PT_NE_BTM_P_001 = Point(0, {ml} / 2, 0);\n";
        //폰툰 반대 끝 점
        script += $"PT_NE_BTM_P_002 = PT_NE_BTM_P_001.copyTranslate(Vec_150.normalise() * ({pontoonLength}));\n";
        // 폰툰 각 끝 옆에 점
        script += $"PT_NE_BTM_P_003 = PT_NE_BTM_P_001.copyTranslate(Vec_060.normalise() * ({dl}));\n";
        script += $"PT_NE_BTM_P_004 = PT_NE_BTM_P_002.copyTranslate(Vec_060.normalise() * ({dl}));\n";
        // 선 001, 002, ELV - 001
        script += $"PT_NE_BTM_C_001 = CreateLineTwoPoints(PT_NE_BTM_P_001, PT_NE_BTM_P_002);\n";
        script += $"PT_NE_BTM_C_002 = CreateLineTwoPoints(PT_NE_BTM_P_003, PT_NE_BTM_P_004);\n";
        script += $"PT_NE_ELV_C_001 = PT_NE_BTM_C_001.copyTranslate(Vector3d(0, 0, {dl}));\n";

        // 면 001, 002  SweepCurve(Curve8, Vector3d(0, 0, 3.5));
        script += $"PT_NE_BTM_PL_001 = SweepCurve(PT_NE_BTM_C_001, Vec_060.normalise() * ({pontoonWidth}));\n";
        script += $"PT_NE_ELV_PL_001 = SweepCurve(PT_NE_BTM_C_001, Vector3d(0, 0, {pontoonHeight}));\n";

        //각 면 복사
        script += $"PT_NE_TOP_PL_001 = PT_NE_BTM_PL_001.copyTranslate(Vector3d(0, 0, {pontoonHeight}));\n";
        script += $"PT_NE_ELV_PL_002 = PT_NE_ELV_PL_001.copyTranslate(Vec_060.normalise() * ({pontoonWidth}));\n";

        //아마 회색이 안쪽이고, 회색은 시계 방향 (오른손 규칙)
        script += $"PT_NE_ELV_PL_001.flipNormal();\n";
        script += $"PT_NE_TOP_PL_001.flipNormal();\n";


        script += "\n// Create Pontoon Beam\n\n";
        // 선 002 를 빔으로!
        script += $"PT_NE_BTM_SF_{PT_NE_BTM_SF_num:000} = Beam(PT_NE_BTM_C_002);";
        ref_PT_NE_BTM_SF_1 = $"PT_NE_BTM_SF_{PT_NE_BTM_SF_num:000}";

        // 이 부분이 문제다. 일단 CURVE 중심으로 생성되는 것은 분명하고, 
        //그 다음 ALIGNOFFSET TOP이 어느 방향인지 모름.
        script += @"
PT_NE_BTM_SF_001.CurveOffset.CurveOffset = AlignedCurveOffset(frFlushTop, 0 m);
PT_NE_BTM_SF_001.rotateLocalX(180 deg);
";

        // 빔 001 복사 : 복사할 개수 식으로 구하기 
        //반복 횟수
        for (int i = 1; i <= (int)(pontoonWidth / dl) - 2; i++)
        {
            PT_NE_BTM_SF_num++;
            script += $"PT_NE_BTM_SF_{PT_NE_BTM_SF_num:000} = {ref_PT_NE_BTM_SF_1}.copyTranslate(Vec_060.normalise()*({dl}*{i}));\n";
        }

        script += $"PT_NE_TOP_SF_{PT_NE_TOP_SF_num:000} = {ref_PT_NE_BTM_SF_1}.copyTranslate(Vector3d(0, 0, {pontoonHeight}));\n";
        ref_PT_NE_TOP_SF_1 = $"PT_NE_TOP_SF_{PT_NE_TOP_SF_num:000}";
        script += $"{ref_PT_NE_TOP_SF_1}.rotateLocalX(180 deg);\n";

        for (int i = 1; i <= (int)(pontoonWidth / dl) - 2; i++)
        {
            PT_NE_TOP_SF_num++;
            script += $"PT_NE_TOP_SF_{PT_NE_TOP_SF_num:000} = {ref_PT_NE_TOP_SF_1}.copyTranslate(Vec_060.normalise()*({dl}*{i}));\n";
        }

        script += $"PT_NE_ELV_SF_001 = Beam(PT_NE_ELV_C_001);\n";
        script += $"PT_NE_ELV_SF_001.CurveOffset.CurveOffset = AlignedCurveOffset(frFlushTop, 0 m);\n";
        script += $"PT_NE_ELV_SF_001.rotateLocalX(-90 deg);\n";

        ref_PT_NE_ELV_SF_1 = "PT_NE_ELV_SF_001";
        for (int i = 1; i <= (int)(pontoonHeight / dl) - 2; i++)
        {
            PT_NE_ELV_SF_num++;
            script += $"PT_NE_ELV_SF_{PT_NE_ELV_SF_num:000} = {ref_PT_NE_ELV_SF_1}.copyTranslate(Vector3d(0, 0, 1).normalise()*({dl}*{i}));\n";
        }

        // 폰툰 바깥쪽 스티프너 생성
        PT_NE_ELV_SF_num++;
        script += $"PT_NE_ELV_SF_{PT_NE_ELV_SF_num:000} = {ref_PT_NE_ELV_SF_1}.copyTranslate(Vec_060.normalise()*({pontoonWidth}));\n";
        ref_PT_NE_ELV_SF_1 = $"PT_NE_ELV_SF_{PT_NE_ELV_SF_num:000}";
        script += $"{ref_PT_NE_ELV_SF_1}.rotateLocalX(180 deg);\n";

        for (int i = 1; i <= (int)(pontoonHeight / dl) - 2; i++)
        {
            PT_NE_ELV_SF_num++;
            script += $"PT_NE_ELV_SF_{PT_NE_ELV_SF_num:000} = {ref_PT_NE_ELV_SF_1}.copyTranslate(Vector3d(0, 0, 1).normalise()*({dl}*{i}));\n";
        }


        script += "\n// Create Pontoon BulkHead\n\n";
        //section-C  BHD
        //가로 점 -> 가로 선 -> plate -> SF 가로,
        script += $"PT_NE_BHD_P_001 = PT_NE_BTM_P_001.copyTranslate(Vec_060.normalise() * ({pontoonWidth}));\n";
        script += $"PT_NE_BHD_C_001 = CreateLineTwoPoints(PT_NE_BTM_P_001, PT_NE_BHD_P_001);\n";
        script += $"PT_NE_BHD_PL_001 = SweepCurve(PT_NE_BHD_C_001, Vector3d(0, 0, {pontoonHeight}));\n";
        script += $"PT_NE_BHD_SF_{PT_NE_BHD_SF_num:000} = Beam(PT_NE_BHD_C_001);\n";
        script += $"PT_NE_BHD_SF_001.moveTranslate(Vector3d(0 , 0 , {pontoonHeight} / 2 ), geUNCONNECTED);\n";

        script += $"PT_NE_BHD_SF_001.rotateLocalX(90 deg);\n";
        script += $"PT_NE_BHD_SF_001.CurveOffset = AlignedCurveOffset(frFlushTop, 0 m);\n";


        // 세로 점 선 생성, 이동 -> SF 세로, 복사 width / dl
        script += $"PT_NE_BHD_P_002 = PT_NE_BTM_P_001.copyTranslate(Vector3d(0, 0, {pontoonHeight}));\n";
        script += $"PT_NE_BHD_C_002 = CreateLineTwoPoints(PT_NE_BTM_P_001, PT_NE_BHD_P_002);\n";
        PT_NE_BHD_SF_num++;
        script += $"PT_NE_BHD_SF_{PT_NE_BHD_SF_num:000} = Beam(PT_NE_BHD_C_002);\n";

        ref_PT_NE_BHD_SF_1 = $"PT_NE_BHD_SF_{PT_NE_BHD_SF_num:000}";
        script += $"{ref_PT_NE_BHD_SF_1}.moveTranslate(Vec_060.normalise() * ({dl}),geUNCONNECTED);\n";
        script += @"PT_NE_BHD_SF_002.rotateLocalX(240 deg);
PT_NE_BHD_SF_002.CurveOffset = AlignedCurveOffset(frFlushTop, 0 m);
";


        for (int i = 1; i <= (int)(pontoonWidth / dl) - 2; i++)
        {
            PT_NE_BHD_SF_num++;
            script += $"PT_NE_BHD_SF_{PT_NE_BHD_SF_num:000} = {ref_PT_NE_BHD_SF_1}.copyTranslate(Vec_060.normalise()*({dl}*{i}));\n";
        }


        script += @"PT_NE_BHD_SET_001 = Set();
PT_NE_BHD_SET_001.add(PT_NE_BHD_PL_001);
";
        for (int i = 1; i <= PT_NE_BHD_SF_num; i++)
        {
            script += $"PT_NE_BHD_SET_001.add(PT_NE_BHD_SF_{i:000});\n";
        }
        script += $"\nPT_NE_BHD_SET_001.moveTranslate(Vec_150.normalise() * ({pontoonLength}* {pontoonBhdSpacing}));\n\n";



        /*
                // 복사 section-C
                script += $"PT_NE_BHD_PL_004 = PT_NE_BHD_PL_003.copyTranslate(Vec_150.normalise() * ({pontoonLength} * {pontoonBhdSpacing}));";
                script += $"PT_NE_BHD_PL_005 = PT_NE_BHD_PL_003.copyTranslate(Vec_150.normalise() * ({pontoonLength}));";
        */
        return script;
    }

    public string PTFrame(double dl, double ml, double pontoonWidth, double pontoonHeight, double pontoonLength)
    {
        script = "";
        script += "// Create Pontoon Frame \n\n";


        // frame 아래 쪽 점 두 개
        script += $"PT_NE_FRM01_P_001 = Point(0, {ml} / 2, 0);\n";
        script += $"PT_NE_FRM01_P_002 = PT_NE_FRM01_P_001.copyTranslate(Vec_060.normalise() * ({pontoonWidth}));\n";

        // 선 , 면 만들기 
        script += $"PT_NE_FRM01_C_001 = createLineTwoPoints(PT_NE_FRM01_P_001, PT_NE_FRM01_P_002);\n";
        script += $"PT_NE_FRM01_PL_001 = SweepCurve(PT_NE_FRM01_C_001, Vector3d(0, 0, {pontoonHeight}));\n";

        // 내부 구멍 뚫을 선 그리기
        //// 가로 관련 점 1, 2 선 - 복사 1 이동 1 
        script += $"PT_NE_FRM01_P_003 = PT_NE_FRM01_P_001.copyTranslate(Vec_060.normalise() * ({frmElvSpacing} + {radiusOfFrm}));\n";
        script += $"PT_NE_FRM01_P_004 = PT_NE_FRM01_P_002.copyTranslate(Vec_240.normalise() * ({frmElvSpacing} + {radiusOfFrm}));\n";
        script += $"PT_NE_FRM01_C_002 = createLineTwoPoints(PT_NE_FRM01_P_003, PT_NE_FRM01_P_004);\n";
        script += $"PT_NE_FRM01_C_003 = PT_NE_FRM01_C_002.copyTranslate(Vector3d(0, 0, {pontoonHeight} - {frmTopSpacing}));\n";
        script += $"PT_NE_FRM01_C_002.moveTranslate(Vector3d(0, 0, {frmBtmSpacing}));\n";

        // 세로 관련 점 1, 2 선 - 복사 1 이동 1 
        script += $"PT_NE_FRM01_P_005 = PT_NE_FRM01_P_001.copyTranslate(Vector3d(0, 0, {frmBtmSpacing} + {radiusOfFrm}));\n";
        script += $"PT_NE_FRM01_P_006 = PT_NE_FRM01_P_001.copyTranslate(Vector3d(0, 0, {pontoonHeight} - ({frmTopSpacing} + {radiusOfFrm})));\n";
        script += $"PT_NE_FRM01_C_004 = createLineTwoPoints(PT_NE_FRM01_P_005, PT_NE_FRM01_P_006);\n";
        script += $"PT_NE_FRM01_C_005 = PT_NE_FRM01_C_004.copyTranslate(Vec_060.normalise() * ({pontoonWidth} - ({frmElvSpacing})));\n";
        script += $"PT_NE_FRM01_C_004.moveTranslate(Vec_060.normalise() * ({frmElvSpacing}));\n";

        // radius 관련 점 1
        script += $"PT_NE_FRM01_P_007 = PT_NE_FRM01_P_003.copyTranslate(Vector3d(0, 0, {frmBtmSpacing} + {radiusOfFrm}));\n";
        script += $"PT_NE_FRM01_C_006 = CreateEllipticArcFromCenterAndEndPoints\n";
        script += $"    (PT_NE_FRM01_P_007\n";
        script += $"    , PT_NE_FRM01_C_004.end1\n";
        script += $"    , PT_NE_FRM01_C_002.end1, true);\n";

        //radius 관련 점 2
        script += $"PT_NE_FRM01_P_008 = PT_NE_FRM01_P_004.copyTranslate(Vector3d(0, 0, {frmBtmSpacing} + {radiusOfFrm}));\n";
        script += $"PT_NE_FRM01_C_007 = CreateEllipticArcFromCenterAndEndPoints\n";
        script += $"    (PT_NE_FRM01_P_008\n";
        script += $"    , PT_NE_FRM01_C_005.end1\n";
        script += $"    , PT_NE_FRM01_C_002.end2, true);\n";

        // radius 관련 점 3
        script += $"PT_NE_FRM01_P_009 = PT_NE_FRM01_P_003.copyTranslate(Vector3d(0, 0, {pontoonHeight} - ({frmTopSpacing} + {radiusOfFrm})));\n";
        script += $"PT_NE_FRM01_C_008 = CreateEllipticArcFromCenterAndEndPoints\n";
        script += $"    (PT_NE_FRM01_P_009\n";
        script += $"    , PT_NE_FRM01_C_003.end1\n";
        script += $"    , PT_NE_FRM01_C_004.end2, true);\n";

        // radius 관련 점 4
        script += $"PT_NE_FRM01_P_010 = PT_NE_FRM01_P_004.copyTranslate(Vector3d(0, 0, {pontoonHeight} - ({frmTopSpacing} + {radiusOfFrm})));\n";
        script += $"PT_NE_FRM01_C_009 = CreateEllipticArcFromCenterAndEndPoints\n";
        script += $"    (PT_NE_FRM01_P_010\n";
        script += $"    , PT_NE_FRM01_C_005.end2\n";
        script += $"    , PT_NE_FRM01_C_003.end2, true);\n";

        //빔 만들기
        script += $"\n";
        script += $"FlatBar_frm.setDefault();\n";


        for (int i = 1; i <= 8; i++)
        {
            script += $"PT_NE_FRM01_FB_{i:000} = Beam(PT_NE_FRM01_C_{i + 1:000});\n";
        }

        // plate 자르기 
        script += $"Validate(PT_NE_FRM01_PL_001.primitivePartCount == 2);\n";
        script += $"PT_NE_FRM01_PL_001.explode(IndexedNameMask(2));\n";
        script += $"Validate(Pl2);\n";
        script += $"Validate(Pl3);\n";
        script += $"Delete(Pl3);\n";
        script += $"Rename(Pl2, \"PT_NE_FRM01_PL_001\");\n";

        script += $"FRM01 = Set();\n";
        for (int i = 1; i <= 8; i++)
        {
            script += $"FRM01.add(PT_NE_FRM01_FB_{i:000});\n";
        }
        script += $"FRM01.add(PT_NE_FRM01_PL_001);\n";


        for (int i = 1; i <= (int)(pontoonLength / pontoonFrameSpacing); i++)
        {
            if (i != pontoonLength / pontoonFrameSpacing * pontoonBhdSpacing)
            {
                if (i < pontoonLength / pontoonFrameSpacing * pontoonBhdSpacing)
                {
                    script += $"MyModelTransformerMap = ObjectNameMap();\n";

                    for (int j = 1; j <= 8; j++)
                    {
                        script += $"MyModelTransformerMap.Add(PT_NE_FRM01_FB_{j:000}, \"PT_NE_FRM{i + 1:00}_FB_{j:000}\");\n";
                    }

                    script += $"MyModelTransformerMap.Add(PT_NE_FRM01_PL_001, \"PT_NE_FRM{i + 1:00}_PL_001\");\n";
                    script += $"ModelTransformer(MyModelTransformerMap).copyTranslate(Vec_150.normalise() * ({pontoonFrameSpacing}*{i}));\n";
                    script += $"FRM{i + 1:00} = Set();\n";

                    for (int j = 1; j <= 8; j++)
                    {
                        script += $"FRM{i + 1:00}.add(PT_NE_FRM{i + 1:00}_FB_{j:000});\n";
                    }
                    script += $"FRM{i + 1:00}.add(PT_NE_FRM{i + 1:00}_PL_001);\n";
                }

                if (i > pontoonLength / pontoonFrameSpacing * pontoonBhdSpacing)
                {
                    script += $"MyModelTransformerMap = ObjectNameMap();\n";

                    for (int j = 1; j <= 8; j++)
                    {
                        script += $"MyModelTransformerMap.Add(PT_NE_FRM01_FB_{j:000}, \"PT_NE_FRM{i:00}_FB_{j:000}\");\n";
                    }

                    script += $"MyModelTransformerMap.Add(PT_NE_FRM01_PL_001, \"PT_NE_FRM{i:00}_PL_001\");\n";
                    script += $"ModelTransformer(MyModelTransformerMap).copyTranslate(Vec_150.normalise() * ({pontoonFrameSpacing}*{i}));\n";
                    script += $"FRM{i:00} = Set();\n";
                    for (int j = 1; j <= 8; j++)
                    {
                        script += $"FRM{i:00}.add(PT_NE_FRM{i:00}_FB_{j:000});\n";
                    }
                    script += $"FRM{i:00}.add(PT_NE_FRM{i:00}_PL_001);\n";
                }


            }
        }
        return script;
    }
}
//-----------------------------이상 폰툰 작업------------------------
//-----------------------------이상 폰툰 작업------------------------
