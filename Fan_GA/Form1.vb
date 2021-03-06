﻿Imports System.Globalization
Imports System.IO
Imports System.Math
Imports System.Threading

Public Class Form1
    Public Shared csv As String

    'Motor gegevens (basis ABB - LC M2BA series / HV HXR series)
    '===========================================================
    Public Shared emotor() As String = {
    "Toerental;Vermogen;FrameSize;Lengte;Geluid Lp;Vollast;Massa;ASdia.;Voet_lengte;Voet_breedte;Voeding",
    "3000;1,1;80M;285;58;2850;17;19;135;35;400v,3ph,50Hz",
    "3000;1,5;90S;310;61;2850;21;24;140;35;400v,3ph,50Hz",
    "3000;2,2;90L;335;61;2850;24;24;165;35;400v,3ph,50Hz",
    "3000;3;100L;380;65;2870;33;28;180;38;400v,3ph,50Hz",
    "3000;4;112M;380;67;2900;42;28;190;50;400v,3ph,50Hz",
    "3000;5,5;132S;465;70;2920;58;38;205;55;400v,3ph,50Hz",
    "3000;7,5;132S;465;70;2920;63;38;205;55;400v,3ph,50Hz",
    "3000;11;160M;645;69;2930;105;42;299;60;400v,3ph,50Hz",
    "3000;15;160M;645;69;2920;118;42;299;60;400v,3ph,50Hz",
    "3000;18,5;160L;645;69;2920;133;42;299;60;400v,3ph,50Hz",
    "3000;22;180M;700;69;2930;178;48;335;65;400v,3ph,50Hz",
    "3000;30;200M;774;72;2955;250;55;355;65;400v,3ph,50Hz",
    "3000;37;200M;774;72;2950;270;55;355;65;400v,3ph,50Hz",
    "3000;45;225S;866;74;2960;335;55;393;74;400v,3ph,50Hz",
    "3000;55;250S;875;75;2970;420;60;441;81;400v,3ph,50Hz",
    "3000;75;280S;1088;77;2978;625;65;506;85;400v,3ph,50Hz",
    "3000;90;280S;1088;77;2976;665;65;506;85;400v,3ph,50Hz",
    "3000;110;315S;1174;78;2982;880;65;558;100;400v,3ph,50Hz",
    "3000;132;315S;1174;78;2982;940;65;558;100;400v,3ph,50Hz",
    "3000;160;315S;1174;78;2981;1025;65;558;100;400v,3ph,50Hz",
    "3000;200;315M;1285;78;2980;1190;65;669;100;400v,3ph,50Hz",
    "3000;250;355S;1494;83;2980;1550;70;662;120;400v,3ph,50Hz",
    "3000;315;355SM;1546;83;2978;1750;70;722;120;400v,3ph,50Hz",
    "3000;355;355SM;1546;83;2975;1750;70;722;120;400v,3ph,50Hz",
    "3000;400;355M;1651;83;2982;2150;70;792;120;400v,3ph,50Hz",
    "3000;450;355M;1651;83;2977;2150;70;792;120;400v,3ph,50Hz",
    "3000;500;400L;1828;85;2980;2850;80;967;140;400v,3ph,50Hz",
    "3000;560;400L;1828;85;2983;2900;80;967;140;400v,3ph,50Hz",
    "3000;630;450L;1985;85;2984;4170;80;1100;140;6kV,3ph,50Hz",
    "3000;710;500L;2320;86;2988;5570;90;1200;160;6kV,3ph,50Hz",
    "3000;800;500L;2320;86;2987;5760;90;1200;160;6kV,3ph,50Hz",
    "3000;900;500L;2320;86;2988;6230;90;1200;160;6kV,3ph,50Hz",
    "3000;1000;500L;2320;86;2987;6690;90;1200;160;6kV,3ph,50Hz",
    "3000;1120;500L;2320;86;2988;6790;90;1200;160;6kV,3ph,50Hz",
    "3000;1250;560L;3250;87;2986;9680;110;1250;180;6kV,3ph,50Hz",
    "3000;1400;560L;3250;87;2989;10030;110;1250;180;6kV,3ph,50Hz",
    "3000;1600;560L;3250;87;2991;11230;110;1250;180;6kV,3ph,50Hz",
    "3000;1800;630L;3750;87;2990;12100;140;1400;200;6kV,3ph,50Hz",
    "3000;2000;630L;3750;87;2990;13000;140;1400;200;6kV,3ph,50Hz",
    "1500;1,1;90S;310;52;1400;21;24;140;35;400v,3ph,50Hz",
    "1500;1,5;90L;335;52;1390;26;24;165;35;400v,3ph,50Hz",
    "1500;2,2;100L;380;53;1430;32;28;180;0;400v,3ph,50Hz",
    "1500;3;100L; 3380;53;1420;36;28;180;0;400v,3ph,50Hz",
    "1500;4;112M; 380;56;1430;45;28;190;50;400v,3ph,50Hz",
    "1500;5,5;132S;465;56;1430;60;38;205;55;400v,3ph,50Hz",
    "1500;7,5;132M;505;59;1440;73;38;240;55;400v,3ph,50Hz",
    "1500;11;160M;645;62;1465;94;42;299;60;400v,3ph,50Hz",
    "1500;15;160L;645;62;1460;103;42;299;60;400v,3ph,50Hz",
    "1500;18,5;180M;700;62;1470;175;48;335;65;400v,3ph,50Hz",
    "1500;22;180L;700;63;1470;161;48;335;65;400v,3ph,50Hz",
    "1500;30;200M;774;63;1475;205;55;355;65;400v,3ph,50Hz",
    "1500;37;225S;866;66;1480;310;60;393;74;400v,3ph,50Hz",
    "1500;45;225S;866;66;1480;330;60;393;74;400v,3ph,50Hz",
    "1500;55;250S;875;67;1480;420;65;441;81;400v,3ph,50Hz",
    "1500;75;280S;1088;68;1484;625;75;506;85;400v,3ph,50Hz",
    "1500;90;280S;1088;68;1483;665;75;506;85;400v,3ph,50Hz",
    "1500;110;315S;1204;70;1487;900;80;558;100;400v,3ph,50Hz",
    "1500;132;315S;1204;70;1487;960;80;558;100;400v,3ph,50Hz",
    "1500;160;315S;1204;70;1487;1000;80;558;100;400v,3ph,50Hz",
    "1500;200;315M;1315;70;1486;1160;90;669;100;400v,3ph,50Hz",
    "1500;250;355S;1594;80;1487;1550;100;662;120;400v,3ph,50Hz",
    "1500;315;355SM;1646;80;1488;1800;100;722;120;400v,3ph,50Hz",
    "1500;355;355SM;1646;80;1486;1800;100;722;120;400v,3ph,50Hz",
    "1500;400;355M;1751;80;1489;2100;100;792;120;400v,3ph,50Hz",
    "1500;450;355M;1751;80;1489;2100;100;792;120;400v,3ph,50Hz",
    "1500;500;355M;1751;80;1489;2100;100;792;120;400v,3ph,50Hz",
    "1500;560;400L;1928;85;1489;3050;100;967;140;400v,3ph,50Hz",
    "1500;630;450L;2120;77;1492;3810;110;1100;140;6kV,3ph,50Hz",
    "1500;710;450L;2120;77;1492;4130;110;1100;140;6kV,3ph,50Hz",
    "1500;800;450L;2120;77;1492;4450;110;1100;140;6kV,3ph,50Hz",
    "1500;900;500L;2455;77;1491;5430;120;1200;160;6kV,3ph,50Hz",
    "1500;1000;500L;2455;77;1492;5690;120;1200;160;6kV,3ph,50Hz",
    "1500;1120;500L;2455;77;1493;6080;120;1200;160;6kV,3ph,50Hz",
    "1500;1250;500L;2455;77;1493;7030;120;1200;160;6kV,3ph,50Hz",
    "1500;1400;500L;2455;77;1493;7420;120;1200;160;6kV,3ph,50Hz",
    "1500;1600;560L;3025;82;1495;9670;160;1250;180;6kV,3ph,50Hz",
    "1500;1800;560L;3025;82;1495;10970;160;1250;180;6kV,3ph,50Hz",
    "1500;2000;560L;3025;82;1496;11730;160;1250;180;6kV,3ph,50Hz",
    "1000;1,1;90L;335;48;920;25;24;165;35;400v,3ph,50Hz",
    "1000;1,5;100L;380;51;930;32;28;180;0;400v,3ph,50Hz",
    "1000;2,2;112M;380;54;940;40;28;190;50;400v,3ph,50Hz",
    "1000;3;132S;465;56;960;55;38;205;55;400v,3ph,50Hz",
    "1000;4;132M;505;56;960;65;38;240;55;400v,3ph,50Hz",
    "1000;5,5;132M;505;56;950;75;38;240;55;400v,3ph,50Hz",
    "1000;7,5;160M;645;59;970;115;42;299;60;400v,3ph,50Hz",
    "1000;11;160L;645;59;970;135;42;299;60;400v,3ph,50Hz",
    "1000;15;180L;700;59;970;177;48;335;65;400v,3ph,50Hz",
    "1000;18,5;200M;774;63;985;245;55;355;65;400v,3ph,50Hz",
    "1000;22;200M;774;63;980;260;55;355;65;400v,3ph,50Hz",
    "1000;30;225S;866;63;985;320;60;393;74;400v,3ph,50Hz",
    "1000;37;250S;875;63;985;415;65;441;81;400v,3ph,50Hz",
    "1000;45;280S;1088;66;990;605;75;506;85;400v,3ph,50Hz",
    "1000;55;280S;1088;66;990;645;75;506;85;400v,3ph,50Hz",
    "1000;75;315S;1204;70;992;830;80;558;100;400v,3ph,50Hz",
    "1000;90;315S;1204;70;992;930;80;558;100;400v,3ph,50Hz",
    "1000;110;315S;1204;70;991;1000;80;558;100;400v,3ph,50Hz",
    "1000;132;315M;1315;68;991;1150;90;669;100;400v,3ph,50Hz",
    "1000;160;355S;1594;75;992;1550;100;662;120;400v,3ph,50Hz",
    "1000;200;355SM;1646;75;992;1800;100;722;120;400v,3ph,50Hz",
    "1000;250;355SM;1646;75;992;1800;100;722;120;400v,3ph,50Hz",
    "1000;315;355M;1751;75;991;2100;100;792;120;400v,3ph,50Hz",
    "1000;355;355M;1751;78;991;2100;100;792;120;400v,3ph,50Hz",
    "1000;400;400L;1928;80;992;2800;100;967;140;400v,3ph,50Hz",
    "1000;450;400L;1928;80;993;3050;100;967;140;400v,3ph,50Hz",
    "1000;500;400L;1928;80;992;3050;100;967;140;400v,3ph,50Hz",
    "1000;560;450L;2120;76;993;3920;110;1100;140;6kV,3ph,50Hz",
    "1000;630;450L;2120;76;993;4160;110;1100;140;6kV,3ph,50Hz",
    "1000;710;450L;2120;76;994;4520;110;1100;140;6kV,3ph,50Hz",
    "1000;800;500L;2455;77;994;5690;120;1200;160;6kV,3ph,50Hz",
    "1000;900;500L;2455;77;994;5890;120;1200;160;6kV,3ph,50Hz",
    "1000;1000;500L;2455;77;994;6180;120;1200;160;6kV,3ph,50Hz",
    "1000;1120;500L;2455;77;994;6760;120;1200;160;6kV,3ph,50Hz",
    "1000;1250;500L;2455;77;995;7350;120;1200;160;6kV,3ph,50Hz",
    "1000;1400;560L;3025;81;996;9720;160;1250;180;6kV,3ph,50Hz",
    "1000;1600;560L;3025;81;996;10670;160;1250;180;6kV,3ph,50Hz",
    "1000;1800;560L;3025;81;996;12160;160;1250;180;6kV,3ph,50Hz",
    "1000;2000;630L;3525;81;996;13200;180;1400;200;6kV,3ph,50Hz",
    "750;4;160M;645;59;715;100;42;299;60;400v,3ph,50Hz",
    "750;5,5;160M;645;59;710;113;42;299;60;400v,3ph,50Hz",
    "750;7,5;160L;645;59;715;126;42;299;60;400v,3ph,50Hz",
    "750;11;180L;700;59;720;177;42;335;65;400v,3ph,50Hz",
    "750;15;200M;774;60;740;250;48;355;65;400v,3ph,50Hz",
    "750;18,5;225S;866;63;730;305;55;393;74;400v,3ph,50Hz",
    "750;22;225S;866;63;730;320;60;393;74;400v,3ph,50Hz",
    "750;30;250S;875;63;735;415;65;441;81;400v,3ph,50Hz",
    "750;37;280S;1088;65;741;605;75;506;85;400v,3ph,50Hz",
    "750;45;280S;1088;65;741;645;75;506;85;400v,3ph,50Hz",
    "750;55;315S;1204;62;742;830;80;558;100;400v,3ph,50Hz",
    "750;75;315S;1204;62;741;930;80;558;100;400v,3ph,50Hz",
    "750;90;315S;1204;64;741;1000;80;558;100;400v,3ph,50Hz",
    "750;110;315M;1315;72;740;1150;90;669;100;400v,3ph,50Hz",
    "750;132;355S;1594;75;742;1550;100;662;120;400v,3ph,50Hz",
    "750;160;355SM;1646;75;742;1800;100;722;120;400v,3ph,50Hz",
    "750;200;355M;1751;75;743;2100;100;792;120;400v,3ph,50Hz",
    "750;250;355M;1751;75;744;2100;100;792;120;400v,3ph,50Hz",
    "750;315;400L;1928;80;744;2800;100;967;140;400v,3ph,50Hz",
    "750;355;400L;1928;80;744;3050;100;967;140;400v,3ph,50Hz",
    "750;400;450L;2120;75;743;3810;110;1100;140;6kV,3ph,50Hz",
    "750;450;450L;2120;75;743;3970;110;1100;140;6kV,3ph,50Hz",
    "750;500;450L;2120;75;744;4530;110;1100;140;6kV,3ph,50Hz",
    "750;560;450L;2120;75;744;4850;110;1100;140;6kV,3ph,50Hz",
    "750;630;450L;2120;75;745;4930;110;1100;140;6kV,3ph,50Hz",
    "750;710;500L;2455;75;745;5970;120;1200;160;6kV,3ph,50Hz",
    "750;800;500L;2455;75;745;6080;120;1200;160;6kV,3ph,50Hz",
    "750;900;500L;2455;75;745;6270;120;1200;160;6kV,3ph,50Hz",
    "750;1000;500L;2455;75;745;6860;120;1200;160;6kV,3ph,50Hz",
    "750;1120;500L;2455;78;745;7830;120;1200;160;6kV,3ph,50Hz",
    "750;1250;560L;3025;78;746;9700;160;1250;180;6kV,3ph,50Hz",
    "750;1400;560L;3025;78;747;10390;160;1250;180;6kV,3ph,50Hz",
    "750;1600;560L;3025;78;747;12110;160;1250;180;6kV,3ph,50Hz",
    "750;1800;630L;3525;80;747;13250;180;1400;200;6kV,3ph,50Hz",
    "750;2000;630L;3525;80;747;14125;180;1400;200;6kV,3ph,50Hz"
    }

    '============= ABB MOTOR SIZES ========================
    'ABB M3BA Industrial performance cast iron motors (pag 104)
    Public Shared ABB_dimen() As String = {
    "Size;A;AA;AB;AC;AE;AF;B;BA;BB;BC;C;CA;CB;D-Tol.;DA;DB;DC;E;EA;EG;EH;F;FA;G;GA;GB;GC;H",
    "71;112;24;136;139;97;139;90;24;110;24;45;104;10;14-j6;11;M5;M4;30;23;12.5;10;5;4;11;16;8.5;12.5;71",
    "80;125;28;154;157;97;157;100;28;125;28;50;136;12.5;19-j6;14;M6;M5;40;30;16;12.5;6;5;15.5;21.5;11;16;80",
    "90S;140;30;170;177;110;177;100;30;150;55;56;156.5;12.5;24-j6;14;M8;M5;50;30;19;12.5;8;5;20;27;11;16;90",
    "90L;140;30;170;177;110;177;125;30;150;55;56;131.5;12.5;24-j6;14;M8;M5;50;30;19;12.5;8;5;20;27;11;16;90",
    "100;160;38;200;197;110;197;140;34;172;34;63;123;16;28-j6;19;M10;M6;60;40;22;16;8;6;24;31;15.5;21.5;100",
    "112;190;41;230;197;110;197;140;34;172;34;70;138;16;28-j6;19;M10;M6;60;40;22;16;8;6;24;31;15.5;21.5;112",
    "132S;216;47;262;261;160;261;140;40;212;76;89;228;16;38-k6;24;M12;M8;80;50;28;19;10;8;33;41;20;27;132",
    "132M;216;47;262;261;160;261;178;40;212;76;89;190;16;38-k6;24;M12;M8;80;50;28;19;10;8;33;41;20;27;132"
    }
    '============= HFB Grease Blocklagers =========================
    'https://www.hfb-waelzlager.de/media/Englisch%20Plummer%20Block%20Housings/bl.pdf
    Public Shared HFB_BL() As String = {
    "Kurzz;da;D;H;HI;H2;L;J;N;A;I.2;J1;b;Lager;kg;scheibe;rpm;Erstfullung;Nachsmierung;s;x;L3;L1",
    "BL25;25;62;50;95;16;155;120;11,5;218;184;135;41;6305;6,5;AS62;11000;50;5;14;7;108;358",
    "BL30;30;72;50;100;18;170;130;15;300;266;210;50;6306;8;AS72;9000;60;5;14;7;186;470",
    "BL35;35;80;60;120;20;190;150;15;330;296;240;50;6307;9;AS80;8500;80;5;16;7;208;530",
    "BL40;40;90;60;120;20;190;150;15;330;296;240;50;6308;10;AS90;7500;110;10;16;7;204;530",
    "BL40S;40;90;60;120;20;190;150;15;450;416;360;50;6308;16;AS90;7500;110;10;16;7;324;530",
    "BL45;45;100;70;140;23;190;150;15;365;331;263;60;6309;14;AS100;6700;140;10;16;7;235;575",
    "BL45/K;45;100;70;140;28;200;160;16;260;226;165;64;6309;12;AS100;6700;140;10;16;7;130;575",
    "BL45/H;45;85;60;118;20;190;150;15;305;282;200;42;6209;14;AS85;7500;120;10;8;15;198;575",
    "BL50;50;110;70;140;23;190;150;15;365;331;263;60;6310;13;AS110;6300;190;10;16;7;231;585",
    "BL50S;50;110;70;140;23;190;150;15;550;516;448;60;6310;21;AS110;6300;190;10;16;7;416;585",
    "BLC50;50;110;80;150;22;230;175;18;455;421;375;75;6310;15;AS110;6300;190;10;16;7;321;585",
    "BL55;55;120;80;160;25;210;170;20;405;371;295;70;6311;22;AS120;5600;240;15;16;6;269;645",
    "BL60;60;130;80;160;25;210;170;20;405;371;295;70;6312;21;AS130;5000;300;15;16;6;265;645",
    "BL60SS;60;130;80;160;25;210;170;20;740;706;595;65;6312;35;AS130;5000;300;15;16;6;600;645",
    "BLC60;60;130;95;175;25;260;200;20;515;481;420;75;6312;25;AS130;5000;300;15;16;6;375;645",
    "BLK60;60;130;80;160;22;224;190;15;365;349;263;62;6312;21;AS130;5000;300;15;16;12;231;645",
    "BL70;70;150;95;190;28;270;210;24;450;416;330;80;6314;31;AS150;4500;480;20;20;6;294;755",
    "BL75;75;160;95;190;28;270;210;24;450;416;330;80;6315;32;AS160;4300;590;20;20;6;290;755",
    "BL80;80;170;112;217;30;290;230;24;490;450;350;75;6316;50;AS170;3800;700;20;20;6;320;805",
    "BLK80;80;170;105;210;30;300;250;20;395;375;295;70;6316;44;AS170;3800;700;20;20;15;227;805",
    "BLL80;80;170;120;240;35;335;280;24;590;566;450;88;6316;75;AS170;3800;700;20;20;16;416;805",
    "BLK90;90;190;120;240;35;355;280;24;440;416;330;78;6318;64;AS190;3400;1000;25;20;16;258;935",
    "BLL90;90;190;120;240;35;335;280;24;590;566;450;88;6318;85;AS190;3400;1000;25;20;16;408;935",
    "BL95;95;200;125;245;35;340;280;24;540;500;400;80;6319;60;AS200;3200;1150;30;25;6;348;985",
    "BL100;100;215;145;290;40;400;335;24;590;562;450;104;6320;95;AS215;3000;1450;35;25;16;386;985",
    "BL110;110;240;145;290;40;400;335;24;590;562;450;104;6322;86;AS240;2600;1900;40;25;16;380;985"
}
    '=========HFB ZGLO OIL Blocklagers======================
    'http://www.hfb.it/eng/pdf/ZLGOe.pdf
    Public Shared HFB_ZGLO() As String = {
    "Gehause;d;d2;d3;d4;d9;A;B;C;L1;H;HI;L;E;EI;L2;L3;s;los lager;vast lager;nut;blech;kg;Oil;length",
    "ZLGO40A;40;50;42;37;M40x1,5;290;295;40;250;80;165;380;230;235;410;135;M16;NU308-C3;6308-C3;KM8;MB8;23;0.9;535;",
    "ZLGO50A;50;60;52;45;M50x1,5;320;348;45;300;95;192;432;260;285;444;175;M16;NU310-C3;6310-C3;KM10;MB10;27;1.5;612",
    "ZLGO55A;55;65;57;50;M55x2;320;348;45;300;95;192;432;260;285;444;175;M16;NU311-C3;6311-C3;KM11;MB11;30;1.5;680",
    "ZLGO65A;65;75;67;60;M65x2;370;390;50;340;110;222;480;300;320;490;205;M20;NU313-C3;6313-C3;KM13;MB13;56;2.2;751",
    "ZLGO70A;70;80;72;65;M70x2;400;450;55;400;120;245;556;330;370;573;265;M20;NU314-C3;6314-C3;KM14;MB14;72;3;805",
    "ZLGO75A;75;85;77;70;M75x2;400;450;55;400;120;245;556;330;370;573;265;M20;NU315-C3;6315-C3;KM15;MB15;70;3;",
    "ZLGO80A;80;90;82;75;M80x2;460;520;60;460;135;272;645;380;430;655;301;M24;NU316-C3;6316-C3;KM16;MB16;105;4;960",
    "ZLGO85A;85;95;87;80;M85x2;460;520;60;460;135;272;645;380;430;655;299;M24;NU317-C3;6317-C3;KM17;MB17;120;4;1007",
    "ZLGO90A;90;105;92;85;M90x2;560;585;70;520;150;307;736;445;490;756;357;M36;NU318-C3;6318-C3;KM18;MB18;140;6;",
    "ZLGO95A;95;110;97;90;M95x2;560;585;70;520;150;307;736;445;490;746;357;M36;NU319-C3;6319-C3;KM19;MB19;145;6;1094",
    "ZLGO100A;100;115;102;95;M100x2;560;585;70;520;150;307;736;445;490;766;268;M36;NU220-C3;6220-C3;KM20;MB20;145;6;1204",
    "ZLGO110A;110;125;112;105;M110x2;560;585;70;520;150;307;736;445;490;746;364;M36;NU222-C3;6222-C3;KM22;MB22;150;6;1142",
    "ZLGO120A;120;140;122;115;M120x2;600;955;75;900;160;330;1105;480;860;1125;730;M36;NU224-C3;6224-C3;KM24;MB24;220;10;1142",
    "ZLGO140A;140;160;142;135;M140x2;690;830;90;750;200;397;1024;550;710;1050;554;M24;NU228-C3;6228-C3;KM28;MB28;350;20;1375"
    }
    '"ZLGO130A;130;150;132;125;M130x2;690;830;90;750;200;397;1024;550;710;1050;554;M42;NU226-C3;6226-C3;KM26;MB26;340;18;",


    '============= ROUND INLET FLANGE ===================
    Public Shared DIN24154R2() As String = {
    "Nominaal;Diam. Uitw.;Diam. Inw.;Diam. Stc;Diam.Gtn;Atl Gtn;Flensdikte;Flensbrdte",
    "DN71;133;73;110;9,5;4;6;30",
    "DN80;142;82;118;9,5;4;6;30",
    "DN90;152;92;128;9,5;4;6;30",
    "DN100;162;102;139;9,5;4;6;30",
    "DN112;175;115;151;9,5;4;6;30",
    "DN125;187;127;165;9,5;4;6;30",
    "DN140;212;142;182;11,5;8;6;35",
    "DN160;232;162;200;11,5;8;6;35",
    "DN180;252;182;219;11,5;8;6;35",
    "DN200;273;203;241;11,5;8;6;35",
    "DN224;297;227;265;11,5;8;6;35",
    "DN250;323;253;292;11,5;8;6;35",
    "DN280;363;283;332;11,5;8;8;40",
    "DN315;398;318;366;11,5;8;8;40",
    "DN355;438;358;405;11,5;8;8;40",
    "DN400;484;404;448;11,5;12;8;40",
    "DN450;534;454;497;11,5;12;8;40",
    "DN500;584;504;551;11,5;12;8;40",
    "DN560;664;564;629;14;16;8;50",
    "DN630;734;634;698;14;16;8;50",
    "DN710;814;714;775;14;16;8;50",
    "DN800;904;804;861;14;24;8;50",
    "DN900;1004;904;958;14;24;8;50",
    "DN1000;1105;1005;1067;14;24;8;50",
    "DN1120;1245;1125;1200;18;32;10;60",
    "DN1250;1375;1255;1337;18;32;10;60",
    "DN1400;1525;1405;1475;18;32;10;60",
    "DN1600;1725;1605;1675;18;40;10;60",
    "DN1800;1925;1805;1875;18;40;10;60",
    "DN2000;2125;2005;2073;18;40;10;60"
    }


    '================ FAN DIMENSIONS =======================
    '0=Type (T20)
    '1=waaier_diameter(635)
    '2=Pers_flens_Lengte(362)
    '3=Pers_flens_Breedte(175)
    '4=Pers_flens_as_vert(450)
    '5=pers_flens-as_hor(466)
    '6=Keel_dia(247)
    '7=Zuig_Flens_dia(300)
    '8=as_Vert(476)
    '9=breedte_huis(545)
    '10= volute r1
    '11= volute r2
    '12= volute r3
    '13= volute r3

    Public Shared fan_dim() As String = {
    "T01;925;770;345;650;813;485;560;815;984; 1068;899;730;0",
    "T12;613;600;300;420;552;465;550;560;706; 300;633;487;0",
    "T16;1030;370;190;710;650;248;300;686;750;761;711;662;0",
    "T17;745;650;300;460;624;465;550;548;726; 826;671;515;0",
    "T20;635;362;175;450;466;247;300;476;545; 580;511;442;0",
    "T21;450;303;225;280;332;282;340;318;380; 423;338;309;0",
    "T25;758;650;345;600;665;430;584;666;804; 874;735;596;497", '4 radii
    "T26;703;308;150;440;468;204;246;488;540; 566;514;462;410", '4 radii
    "T27;805;280;148;545;498;203;230;522;565; 580;542;503;0",
    "T28;660;315;250;425;425;425;315;466;523; 553;495;437;0",
    "T30;758;650;345;600;665;430;584;665;804; 874;735;596;496", '4 radii
    "T31;758;650;345;600;665;430;584;666;804; 874;735;596;496", '4 radii
    "T32;758;650;345;600;665;430;584;664;804; 874;735;596;496", '4 radii
    "T33;758;650;483;600;665;500;768;665;804; 874;735;596;496", '4 radii
    "T34;758;650;526;600;665;500;768;665;804; 874;735;596;496", '4 radii
    "T35;600;200;105;475;390;225;140;442;456; 473;425;0;0",     '2 radii
    "T36;760;435;250;540;557;336;398;570;653; 694;612;529;0",
    "GW(A);485;76;40;300;260;62;62;278;278;   278;0;0;0",       '1 radius
    "Galak;1200;665;300;820;875;435;500;1018;886; 1086;954;822;690"}

    '"Motor-foot;L;W",
    Public Shared motor_foot() As String = {
    "80M;135;35",
    "90S;140;35",
    "90L;165;35",
    "100L;180;38",
    "112M;190;50",
    "132S;205;55",
    "132M;240;55",
    "160M;299;60",
    "160L;299;60",
    "180M;335;65",
    "180L;335;65",
    "200M;355;65",
    "200L;378;65",
    "225S;393;74",
    "250S;441;81",
    "280S;506;85",
    "315S;558;100",
    "315M;669;100",
    "315L;851;100",
    "355S;662;120",
    "355SM;722;120",
    "355M;792;120",
    "400M;737;120",
    "400L;967;140",
    "450L;1100;140",
    "500L;1200;160",
    "560L;1250;180",
    "630L;1400;200"
    }

    'See http://www.escogroup.com/sites/default/files/datasheet/dmu.pdf
    Public Shared Escodisc_DMU() As String = {
    "type;dmax;dmin;Tn;Tp;rpm1;rpm2;deg;mm;mm;wr2;kg;A;B;D;E;G;H;K;L;S;X;*;8",
    "DMU_38-45;45;0;190;290;8000;16000;2x0.75;2.4;0.8;0.0015;3.08;170;88;58.5;35;100;6.7;21;41;70.6;86.6",
    "DMU_45-55;55;0;330;500;6800;13600;2x0.5;2.0;0.8;4.0;4.98;190;102;69.5;45;100;6.5;37;61;71;87",
    "DMU_55-65;65;0;750;1120;6000;12000;2x0.5;2.4;0.8;8.0;8.0;200;123;82;50;100;7;48;72;64;86",
    "DMU_65-75;75;25;1330;2000;5000;10000;2x0.5;2.6;0.8;18;12.05;220;147;97.5;60;100;9;54;86;60;82",
    "DMU_75-90;90;32;2200;3320;4300;8600;2x0.5;3.0;1.1;0.04;20.12;280;166;113;70;140;10;65;98;88;120",
    "DMU_85-105;105;38;3500;5200;3600;7200;2x0.5;4.0;1.1;84;30.65;310;192;132;85;140;13;76;116;80;114",
    "DMU_95-105;105;45;5600;8400;3200;6400;2x0.5;4.0;1.1;136;39.5;330;224;133;95;140;14;94;134;76;112",
    "DMU_110-120;120;55;8000;12000;2800;5600;2x0.5;4.4;1.4;262;59.8;400;244;154;110;180;15.5;108;156;103;149",
    "DMU_125-135;135;65;10900;16400;2500;5000;2x0.5;5.2;1.4;434;79.04;430;273;175;125;180;19;123;171;96;142",
    "DMU_140-160;160;65;14200;21200;2300;4600;2x0.5;6.6;2.0;779;115.5;530;303;196;140;250;20;143;191;160;210",
    "DMU_160-185;185;80;19800;29600;2000;4000;2x0.5;6.8;2.0;1.436;163.6;570;340;228;160;250;20;165;221;154;210"
    }

    Public Shared Cooling_disk() As String = {
   "Typ;D;Dmin;d;dmax;d1;B;Bmin;b;Nmax;kgs",
    "K-150;150;106;10;50;60;30;23;3;8900;0.7",
    "K-200;200;106;19;50;60;30;23;3;6700;1.1",
    "K-250;250;150;28;70;85;34;29;3;5350;1.9",
    "K-315;315;180;38;95;112;54;39;5;4250;3.8",
    "K-400;400;340;53;125;145;68;45;4;3350;8.6",
    "K-400SO;400;375;120;155;170;74;45;10;3350;9",
    "K-500;500;380;80;165;180;68;48;5;2700;12",
    "K-630;630;500;100;200;225;78;52;5;2100;19"
    }

    Public Shared _mid As Point
    Public Shared _pic_scale As Double

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ListView1 As ListView
        ListView1 = New ListView With {
            .Location = New Point(10, 10),
            .Size = New Size(150, 150)
        }
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")

        '-------Fill motor combobox, group------------------
        Dim words() As String
        Dim separators() As String = {";"}

        For hh = 0 To emotor.Length - 1
            words = emotor(hh).Split(separators, StringSplitOptions.None)
            ComboBox1.Items.Add(words(1)) 'Fill combobox 
        Next hh
        ComboBox1.SelectedIndex = 1

        '-------Fill Coupling, group------------------
        For hh = 0 To Escodisc_DMU.Length - 1
            words = Escodisc_DMU(hh).Split(separators, StringSplitOptions.None)
            ComboBox2.Items.Add(words(0)) 'Fill combobox 
        Next hh
        ComboBox2.SelectedIndex = 1

        '--------Fill combobox, Fan Tmodels------
        For hh = 0 To fan_dim.Length - 1
            words = fan_dim(hh).Split(separators, StringSplitOptions.None)
            ComboBox3.Items.Add(words(0)) 'Fill combobox 
        Next hh
        ComboBox3.SelectedIndex = 4

        '--------Fill combobox, HFB BL series bearing Block------
        For hh = 0 To HFB_BL.Length - 1
            words = HFB_BL(hh).Split(separators, StringSplitOptions.None)
            ComboBox4.Items.Add(words(0)) 'Fill combobox 
        Next hh
        ComboBox4.SelectedIndex = 4

        '--------Fill combobox, HFB BL series bearing Block------
        For hh = 0 To HFB_ZGLO.Length - 1
            words = HFB_ZGLO(hh).Split(separators, StringSplitOptions.None)
            ComboBox5.Items.Add(words(0)) 'Fill combobox 
        Next hh
        ComboBox5.SelectedIndex = 4

        '--------Fill combobox, Cooling disk------
        'http://www.troester-maschinenbau.de
        For hh = 0 To Cooling_disk.Length - 1
            words = Cooling_disk(hh).Split(separators, StringSplitOptions.None)
            ComboBox6.Items.Add(words(0)) 'Fill combobox 
        Next hh
        ComboBox6.SelectedIndex = 2

        PictureBox16.Size = CType(New Point(400, 400), Size)
        _mid.X = CInt(PictureBox16.Width / 2)   'Midpoint canvas
        _mid.Y = CInt(PictureBox16.Height / 2)  'Midpoint canvas

        _pic_scale = CInt(NumericUpDown3.Value)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim words() As String
        Dim separators() As String = {";"}

        '===== motor diemnsions =======
        words = emotor(ComboBox1.SelectedIndex).Split(separators, StringSplitOptions.None)
        TextBox3.Text = words(0)
        TextBox4.Text = words(1)
        TextBox6.Text = words(2)
        TextBox8.Text = words(3)
        TextBox9.Text = words(4)
        TextBox10.Text = words(5)
        TextBox11.Text = words(6)
        TextBox12.Text = words(7)
        TextBox13.Text = words(8)
        TextBox14.Text = words(9)
        TextBox5.Text = words(10)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim words() As String
        Dim separators() As String = {";"}

        words = Escodisc_DMU(ComboBox2.SelectedIndex).Split(separators, StringSplitOptions.None)
        TextBox7.Text = words(1)    'Max diameter
        TextBox15.Text = words(20)  'S-maat
        TextBox16.Text = words(11)  'Weight

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Update_dimensions()
    End Sub
    'Grease bearing blocks
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Dim words() As String
        Dim separators() As String = {";"}

        words = HFB_BL(ComboBox4.SelectedIndex).Split(separators, StringSplitOptions.None)
        TextBox30.Text = words(1)  'da shaft diameter
        TextBox35.Text = words(2)  'Bearing OD
        TextBox29.Text = words(4)  'Cl shaft height
        TextBox17.Text = words(9)  'A, Housing length
        TextBox36.Text = words(6)  'L, Housing width
        TextBox37.Text = words(22) 'L1, Shaft length
        TextBox38.Text = words(14) 'Weight
        TextBox39.Text = words(13) 'Bearing
    End Sub
    'Oil bearing blocks
    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim words() As String
        Dim separators() As String = {";"}

        words = HFB_ZGLO(ComboBox5.SelectedIndex).Split(separators, StringSplitOptions.None)
        TextBox31.Text = words(4)  'Shaft diameter
        TextBox32.Text = words(2)  'Bearing ID
        TextBox33.Text = words(10) 'H Shaft height
        TextBox40.Text = words(15) 'L2 Housing length
        TextBox41.Text = words(6)  'A Housing width
        TextBox42.Text = words(24) 'Shaft length
        TextBox43.Text = words(22) 'Weight
        TextBox44.Text = words(18) 'Bearing floating
        TextBox45.Text = words(19) 'Bearing fixed
    End Sub
    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        Dim words() As String
        Dim separators() As String = {";"}

        words = Cooling_disk(ComboBox6.SelectedIndex).Split(separators, StringSplitOptions.None)
        TextBox47.Text = words(3)  'Shaft diameter min
        TextBox48.Text = words(4)  'Shaft diameter max
        TextBox46.Text = words(6)  'Shaft diameter max
    End Sub

    Private Sub Update_dimensions()
        Dim factor As Double
        Dim dia_tschets As Double
        Dim dia_actual As Double
        Dim words() As String
        Dim separators() As String = {";"}

        If ComboBox3.SelectedIndex > 0 Then
            words = fan_dim(ComboBox3.SelectedIndex).Split(separators, StringSplitOptions.None)
            dia_actual = NumericUpDown1.Value
            dia_tschets = CDbl(words(1))
            factor = dia_actual / dia_tschets
            TextBox34.Text = factor.ToString("0.000")

            '0=Type (T20)
            '1=waaier_diameter(635)
            '2=Pers_flens_Lengte(362)
            '3=Pers_flens_Breedte(175)
            '4=Pers_flens_as_vert(450)
            '5=pers_flens-as_hor(466)
            '6=Keel_dia(247)
            '7=Zuig_Flens_dia(300)
            '8=as_Vert(476)
            '9=breedte_huis(545)

            TextBox1.Text = (CDbl(words(1)) * factor).ToString("0")
            TextBox2.Text = (CDbl(words(8)) * factor).ToString("0")  'Zuimond hoogte
            TextBox18.Text = (CDbl(words(2)) * factor).ToString("0") 'Persflens Lengte
            TextBox19.Text = (CDbl(words(3)) * factor).ToString("0") 'Persflens Breedte
            TextBox20.Text = (CDbl(words(4)) * factor).ToString("0") 'Persflens-shaft
            TextBox21.Text = (CDbl(words(9)) * factor).ToString("0") 'E
            TextBox22.Text = (CDbl(words(8)) * factor).ToString("0") 'H
            TextBox57.Text = (CDbl(words(5)) * factor).ToString("0")

            TextBox21.Text = (CDbl(words(10)) * factor).ToString("0") 'Volute
            TextBox22.Text = (CDbl(words(11)) * factor).ToString("0") 'Volute
            TextBox23.Text = (CDbl(words(12)) * factor).ToString("0") 'Volute
            TextBox24.Text = (CDbl(words(13)) * factor).ToString("0") 'Volute

            TextBox28.Text = (CDbl(words(7)) * factor).ToString("0") 'Inlet flange
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Update_dimensions()
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        Update_dimensions()
    End Sub
    'Save file dialog
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim filename As String = "c:\Temp\Onshape.csv"
        Calc_casing()

        If File.Exists(filename) Then
            File.Delete(filename)
        End If
        Try
            Dim sw As New StreamWriter(filename)
            sw.Write(csv)
            sw.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'MessageBox.Show(filename & " is succesfully written")
    End Sub
    Private Sub Calc_casing()
        'https://nl.wikipedia.org/wiki/Archimedes-spiraal
        'r =r1 + t*(r2-r1)	
        'the onshape unit is meter !!
        Dim r, r1, r2 As Double
        Dim delta_x, delta_y, z As Double
        Dim a, b, c As Point
        Dim hook As Double
        Dim n As Point
        Dim inlet_d As Integer
        Dim onshape As Double = 1000 'Dimension in [m]
        '================
        Dim pic As Bitmap = New Bitmap(1000, 1000)  'New Clean Sheet
        Dim kleur As Color = Color.Yellow

        csv = String.Empty
        Double.TryParse(TextBox27.Text, r1) 'smal radius
        Double.TryParse(TextBox25.Text, r2) 'big radius
        Integer.TryParse(TextBox28.Text, inlet_d) 'big radius

        hook = NumericUpDown4.Value             'Rotation
        _pic_scale = NumericUpDown3.Value       'Picture scale

        TextBox58.Text = "Mid point= " & _mid.X.ToString("0") & "," & _mid.Y.ToString("0") & vbCrLf 'Read out for testing

        If r1 > 0 Then  'For fast program startup
            '======== volute =============

            For i As Double = 1 To 360 Step 2
                r = r1 + i / 360 * (r2 - r1)
                delta_y = r * Sin(i / 180 * PI)
                delta_x = r * Cos(i / 180 * PI)

                '====== onshape export ========
                csv = csv & "volute, " & CInt(delta_x / onshape).ToString & ", " & CInt(delta_y / onshape).ToString & ", " & CInt(0).ToString & vbCrLf

                '====== Picture box ========
                n.X = CInt(delta_x)
                n.Y = CInt(delta_y)

                n = Rotate2(n, hook, i)            'Rotate
                n.X = CInt(n.X / _pic_scale)    'Scale x for picturebox
                n.Y = CInt(n.Y / _pic_scale)    'Scale y for picturebox
                n = Move_to_center(n)           'Move for picturebox

                pic.SetPixel(n.X, n.Y, kleur)
                PictureBox16.Image = pic
            Next

            '======== Outlet rectangle flange =============
            Dim p, q, s As Double
            Double.TryParse(TextBox20.Text, p) 'x off set inlet flange
            Double.TryParse(TextBox57.Text, q) 'CL flange= CL shaft vertikal
            Double.TryParse(TextBox18.Text, s) 'flange height

            '==== start point flange =====
            a.X = CInt(p)
            a.Y = CInt(q + s / 2)

            a = Rotate(a, hook)
            a = Move_to_center(a)

            '==== end point flange =====
            b.X = CInt(p)
            b.Y = CInt(q - s / 2)
            b = Rotate(b, hook)
            b = Move_to_center(b)

            csv = csv & "volute, " & CInt(a.X / onshape).ToString & ", " & CInt(a.Y / onshape).ToString & ", " & CInt(0).ToString & vbCrLf
            csv = csv & "volute, " & CInt(b.X / onshape).ToString & ", " & CInt(b.Y / onshape).ToString & ", " & CInt(0).ToString & vbCrLf

            Draw_line(pic, CInt(a.X), CInt(a.Y), CInt(b.X), CInt(b.Y))  'Picture outlet flange

            '======== inlet flange diameter =============
            For i As Integer = 0 To 360
                r = inlet_d / 2
                c.Y = CInt(r * Sin(i / 180 * PI))
                c.X = CInt(r * Cos(i / 180 * PI))
                z = 0.000
                csv = csv & "Inlet, " & CInt(c.X / onshape).ToString & ", " & CInt(c.Y / onshape).ToString & ", " & CInt(z / onshape).ToString & vbCrLf
            Next
            Draw_circle(pic, inlet_d / 2)        'Picture Inlet flange
        End If
    End Sub

    Private Sub Draw_circle(ByVal pic As Bitmap, ByVal c_radius As Double)
        Dim n As Point
        Dim c As Color = Color.White

        For i As Integer = 0 To 360 Step 5
            n.X = CInt(_mid.X + (c_radius * Cos(i / 180 * PI)) / _pic_scale)
            n.Y = CInt(_mid.Y + (c_radius * Sin(i / 180 * PI)) / _pic_scale)

            n = Check_inside_pic(n)
            pic.SetPixel(n.X, n.Y, c)
            PictureBox16.Image = pic
        Next
    End Sub

    Private Sub Draw_line(ByVal pic As Bitmap, ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim p6, p7 As Point

        Dim g As Graphics = Graphics.FromImage(pic)
        Dim myPen As Pen = New Pen(Color.Blue, 3)

        p6.X = CInt(_mid.X - (x1 / _pic_scale)) 'Start point
        p6.Y = CInt(_mid.Y - (y1 / _pic_scale)) 'Start point

        p7.X = CInt(_mid.X - (x2 / _pic_scale)) 'End point
        p7.Y = CInt(_mid.Y - (y2 / _pic_scale)) 'End point

        p6 = Check_inside_pic(p6)
        p7 = Check_inside_pic(p7)

        g.DrawLine(myPen, p6.X, p6.Y, p7.X, p7.Y)
        PictureBox16.Image = pic
    End Sub


    Private Sub TabPage10_Enter(sender As Object, e As EventArgs) Handles TabPage10.Enter
        Dim vol As Double
        TextBox49.Text = ComboBox3.SelectedItem.ToString    'Fan type
        TextBox50.Text = NumericUpDown1.Value.ToString      'diameter waaier
        TextBox51.Text = NumericUpDown2.Value.ToString      'insulation
        TextBox52.Text = ComboBox4.SelectedItem.ToString    'BL bearingblock
        TextBox53.Text = ComboBox5.SelectedItem.ToString    'ZGLO bearingblock
        TextBox54.Text = ComboBox6.SelectedItem.ToString    'Cooling disk
        TextBox55.Text = ComboBox2.SelectedItem.ToString    'Coupling
        TextBox56.Text = ComboBox1.SelectedItem.ToString    'Motor

        TextBox25.Text = TextBox21.Text    'Volute biggest
        TextBox26.Text = TextBox22.Text    'Volute middle

        vol = CDbl(TextBox23.Text)
        If vol = 0 Then vol = CDbl(TextBox23.Text)
        If vol = 0 Then vol = CDbl(TextBox22.Text)

        TextBox27.Text = vol.ToString("0")    'Volute small
        Calc_casing()
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        Calc_casing()
    End Sub

    Private Sub TabControl1_Enter(sender As Object, e As EventArgs) Handles TabControl1.Enter
        Calc_casing()
    End Sub
    'Opgelet de rotatie_hoek wordt in graden ingevuld
    'Rotation cartesian coordinate system
    Private Function Rotate(ByVal input As Point, rotatie_hoek As Double) As Point
        Dim vektor_length, vektor_angle, new_angle As Double
        Dim neww As Point
        Dim q As String = "-"

        vektor_length = Sqrt(CDbl(input.X) ^ 2 + CDbl(input.Y) ^ 2)
        vektor_angle = Asin(Abs(input.Y) / vektor_length) * 180 / PI

        If vektor_length > 1 Then

            'CALCULATION IS DONE IN Q1 THEN WE ROTATE TO ACTUAL QUARTER
            'If input.X > 0 And input.Y > 0 Then vektor_angle += 0
            'If input.X > 0 And input.Y < 0 Then vektor_angle += 90
            'If input.X < 0 And input.Y < 0 Then vektor_angle += 180
            'If input.X < 0 And input.Y > 0 Then vektor_angle += 270


            If input.X > 0 And input.Y > 0 Then vektor_angle -= 0
            If input.X < 0 And input.Y > 0 Then vektor_angle -= 90
            If input.X < 0 And input.Y < 0 Then vektor_angle -= 180
            If input.X > 0 And input.Y < 0 Then vektor_angle -= 270




            new_angle = rotatie_hoek + vektor_angle
            'TextBox58.Text &= "Hoek rotatie= " & rotatie_hoek.ToString("0") & " vektorhoek=" & vektor_angle.ToString("0") & " V_length" & vektor_length.ToString("0") '& vbCrLf

            neww.X = CInt(vektor_length * Cos(new_angle / 180 * PI))
            neww.Y = CInt(vektor_length * Sin(new_angle / 180 * PI))

            'TextBox58.Text &= " neww.x= " & neww.X.ToString("0") & " neww.Y=" & neww.Y.ToString("0") & vbCrLf


            Dim s As String
            s = "Input vektor= " & input.X.ToString & ", " & input.Y.ToString & vbCrLf
            s &= "vektor length= " & vektor_length.ToString("0") & " vektor angle= " & vektor_angle.ToString("0") & vbCrLf
            s &= "rotatiehoek= " & rotatie_hoek.ToString("0") & vbCrLf
            s &= "result vektor= " & neww.X.ToString & ", " & neww.Y.ToString & vbCrLf
            s &= "berekende hoek= " & vektor_angle.ToString("0") & ", newhook= " & new_angle.ToString("0") & vbCrLf
            s &= "kwadrant= " & q
        Else
            neww.X = 0
            neww.Y = 0
        End If

        Return neww
    End Function
    Private Function Rotate2(ByVal input As Point, rhoek As Double, va As Double) As Point
        'https://en.wikipedia.org/wiki/Cartesian_coordinate_system
        Dim neww As Point
        Dim le, hoek2 As Double

        le = Sqrt(CDbl(input.X) ^ 2 + CDbl(input.Y) ^ 2)
        rhoek = rhoek / 180 * PI            '[radiaal] rotatie
        va = va / 180 * PI                  '[radiaal] orginele vektor hoek
        hoek2 = (rhoek + va)                '[radiaal]

        neww.X = CInt(le * Cos(hoek2))
        neww.Y = CInt(le * Sin(hoek2))

        If neww.X < 0 Then neww.X *= -1

        TextBox58.Text &= "input.x= " & input.X.ToString("0") & "[mm] "
        TextBox58.Text &= "input.Y= " & input.Y.ToString("0") & "[mm] "
        ' TextBox58.Text &= "Rot-hoek= " & rhoek.ToString("0.00") & "[rad] "
        TextBox58.Text &= " vektor-hoek=" & va.ToString("0.00") & "[rad] "
        TextBox58.Text &= " V_length" & le.ToString("0") & "[mm] "
        TextBox58.Text &= " Hoek2= " & hoek2.ToString("0.00") & "[rad] "
        TextBox58.Text &= " neww.X= " & neww.X.ToString("0.00") & "[mm] "
        TextBox58.Text &= " neww.Y= " & neww.Y.ToString("0.00") & "[mm] " & vbCrLf
        Return neww
    End Function

    Private Function Move_to_center(ByVal f As Point) As Point
        Dim w As Point

        'Move to center picture
        w.X = _mid.X - f.X
        w.Y = _mid.Y - f.Y

        w = Check_inside_pic(w) 'Still inside picture ?
        Return w
    End Function

    Private Function Check_inside_pic(ByVal p As Point) As Point
        Dim a As String = "OK"

        If p.X < 0 Then
            p.X = 0
            a = "x= negative"
        End If
        If p.Y < 0 Then
            p.Y = 0
            a = "y= negative"
        End If

        If p.X >= PictureBox16.Width Then
            p.X = PictureBox16.Width
            a = "too wide"
        End If
        If p.Y >= PictureBox16.Height Then
            p.Y = PictureBox16.Height
            a = "too high"
        End If

        If a.Length > 5 Then TextBox58.Text &= a    'Only message if problem exists
        Return (p)
    End Function

    Private Sub NumericUpDown4_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown4.ValueChanged
        Calc_casing()
    End Sub

End Class
