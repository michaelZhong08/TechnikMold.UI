
-------------------项目阶段记录-------------------
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('CAD' ,'1' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('CAM' ,'2' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('采购' ,'3' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('开粗' ,'4' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('CNC开粗' ,'5' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('热处理' ,'6' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('磨床' ,'7' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('CNC' ,'8' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('EDM' ,'9' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('WEDM' ,'10' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('装配' ,'11' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('FOT' ,'12' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('OTS交样' ,'13' ,1)
INSERT INTO [Mold].[dbo].[Phases] ([Name] ,[Sequence] ,[Enabled]) VALUES ('PPAP' ,'14' ,1)

-------------------项目角色记录-------------------
INSERT INTO [Mold].[dbo].[Roles] ([Name] ,[Enabled] ,[ProjectBased]) VALUES ('项目经理' ,1 ,1)
INSERT INTO [Mold].[dbo].[Roles] ([Name] ,[Enabled] ,[ProjectBased]) VALUES ('技术负责人' ,1 ,1)
INSERT INTO [Mold].[dbo].[Roles] ([Name] ,[Enabled] ,[ProjectBased]) VALUES ('钳工' ,1 ,1)

-------------------部门记录-------------------
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('管理' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('CAD' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('CAM' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('采购' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('开粗' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('CNC开粗' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('CNC' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('EDM' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('WEDM' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('质检' ,1);
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('装配' ,1)
INSERT INTO [Mold].[dbo].[Departments] ([Name] ,[Enabled]) VALUES ('仓库' ,1)

-------------------材料记录-------------------
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('635' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('2083' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('2316' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('2711' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('8402' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('8407' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('2083ESR' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('2344EFS' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('2344ESR' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('618T' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('718HH' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Al' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Brass' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('CALDIE' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Cr12' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Crwmn' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Cu' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('DC11' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('DHA1' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('EM38' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('GF' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('钨钢G20C' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('M310' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('M310ESR' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('M333' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Magnet' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('MP40' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('NAK80' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('P20' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('PMMA' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('RoyAlloy' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('S136' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('S50C' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('SKD11' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('SKD61' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('SKH51' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('S-STAR' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('STD' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('SUS631' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('Unimax' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('W302' ,1);
INSERT INTO [Mold].[dbo].[Materials] ([Name] ,[Enabled]) VALUES ('XW42' ,1);

-------------------硬度记录-------------------
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC55-57' ,(select MaterialID from Materials where Name='635') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='2083') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='2083') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC26-32' ,(select MaterialID from Materials where Name='2316') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC35-38' ,(select MaterialID from Materials where Name='2711') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='8402') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='8407') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='8407') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='2083ESR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='2083ESR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='2344EFS') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='2344ESR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='2344ESR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC28-32' ,(select MaterialID from Materials where Name='618T') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC33-37' ,(select MaterialID from Materials where Name='718HH') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC56-58' ,(select MaterialID from Materials where Name='CALDIE') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC58-60' ,(select MaterialID from Materials where Name='Cr12') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC58-60' ,(select MaterialID from Materials where Name='Crwmn') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC58-60' ,(select MaterialID from Materials where Name='DC11') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='DHA1') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC35-38' ,(select MaterialID from Materials where Name='EM38') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRA82-85' ,(select MaterialID from Materials where Name='钨钢G20C') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='M310') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC52-54' ,(select MaterialID from Materials where Name='M310') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='M310ESR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='M310ESR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='M333') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC52-54' ,(select MaterialID from Materials where Name='M333') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC33-38' ,(select MaterialID from Materials where Name='MP40') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC36-42' ,(select MaterialID from Materials where Name='MP40') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC38-42' ,(select MaterialID from Materials where Name='NAK80') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC28-30' ,(select MaterialID from Materials where Name='P20') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC35-37' ,(select MaterialID from Materials where Name='RoyAlloy') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='S136') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC16-18' ,(select MaterialID from Materials where Name='S50C') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC58-60' ,(select MaterialID from Materials where Name='SKD11') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='SKD61') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC60-62' ,(select MaterialID from Materials where Name='SKH51') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC52-54' ,(select MaterialID from Materials where Name='S-STAR') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC54-56' ,(select MaterialID from Materials where Name='Unimax') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC56-58' ,(select MaterialID from Materials where Name='Unimax') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC48-50' ,(select MaterialID from Materials where Name='W302') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC50-52' ,(select MaterialID from Materials where Name='W302') ,1); 
INSERT INTO [Mold].[dbo].[Hardnesses] ([Value] ,[MaterialID] ,[Enabled]) VALUES ('HRC58-60' ,(select MaterialID from Materials where Name='XW42') ,1); 


-------------------零件类型-建议编码-------------------
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Mold Assy','TOP','0000');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Mold Base Assy','Moldbase','1000');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Fixed Half Mold Base Assy','FixHalf','1001');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Move Half Mold Base Assy','MoveHalf','1002');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Fixed Half Mold Base Misc','F-Misc','1003');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Move Half Mold Base Misc','M-Misc','1004');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Public Mold Base Misc','Pub-Misc','1005');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('T Plate','Tplate','1010');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('A Plate','Aplate','1020');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('A Support Plate','-Obligate-','1021');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Manifold Plate','Rplate','1080');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Runner Stripper Plate','Splate','1090');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('B Plate','Bplate','1030');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('B Support Plate','-Obligate-','1031');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('U Plate','Uplate','1100');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('E Plate','Eplate','1040');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('F Plate','Fplate','1050');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('C Plate','Cplate','106L~106R');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('L Plate','Lplate','1070');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Fixed Insulating Plate','FIPlate','1011');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Move Insulating Plate','MIPlate','1012');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Return Pin','Return-Pin','1115');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Pin','-Obligate-','1113~1114');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Bushing','-Obligate-','1116~1117');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('RPlate Guide Bushing','RP-Guide-Bushing','1118');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('RPlate Guide Bushing Off','RP-Guide-Bushing-Off','1119');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('SPlate Guide Bushing','SP-Guide-Bushing','1120');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('SPlate Guide Bushing Off','SP-Guide-Bushing-Off','1121');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('APlate Guide Bushing','AP-Guide-Bushing','1122');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('APlate Guide Bushing Off','AP-Guide-Bushing-Off','1123');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Pin','Guide-Pin','1124');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Pin Off','Guide-Pin-Off','1125');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('BPlate Guide Bushing','BP-Guide-Bushing','1126');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('BPlate Guide Bushing Off','BP-Guide-Bushing','1127');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('UPlate Guide Bushing','UP-Guide-Bushing','1128');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('UPlate Guide Bushing Off','UP-Guide-Bushing-Off','1129');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Sleeve','Guide-Sleeve','1130');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Support Pillar','Support-Pillar','1131~1135');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Lifter Bar','Lifter-Bar','1150');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Locating Ring','Locating-Ring','1151');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Sprue Bush','Sprue-Bush','1152');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Ejector Connector','Ejector -Connector','1153');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('KO Bush','KO-Bush','1154');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Misc Part','MiscPart','1155~1199');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Wire Plate','Wire-Plate','1082~1085');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Housing Base','Housing-Base','1086~1089');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Hot Runner','Hot-Runner','1091');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Hot Tip','Hot-Tip','1092~1095');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Name Plate','Name-Plate','1096~1099');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Layout','Layout','2000');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Prod','Prod01(Prod#01~#10)','-');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Product','Product01(Shk#01~#10)','-');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Cooling Layout','Cooling-Layout','7000');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Main Cavity','Main-Cavity','3101~3109');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Cavity Insert Asm','Cavity-Insert-Asm','-');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Cavity Insert','Cavity-Insert','3111~3190');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Cavity Block','Cavity-Block ','3191~3199');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Main Core','Main-Core','4201~4209');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Core Insert Asm','Core-Insert-Asm','-');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Core Insert','Core-Insert','4211~4290');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Core Block','Core-Block','4291~4299');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Lifter Asm','Lifter-Asm01(#01~#10)','-');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Lifter;Lifter Insert','Lifter','5401~5420');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Lifter Base','Lifter-Base','5421~5440');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Lifter Guide','Lifter-Guide','5441~5460');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Lifter Misc Part','Lifter-Misc','5461~5499');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Asm','Slider-Asm01(#01~#10)','-');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Sldier; Slider Base','Slider;Slider-Base','6301~6310');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Insert','Slider-Insert','6311~6330');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Lock','Slider-Lock','6331~6340');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Wedge','Slider-Wedge','6341~6350');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Rail','Slider-Rail','6351~6360');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Bottom Wear','Slider-BT-Waer','6361~6370');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Back Wear','Slider-BK-Waer','6371~6380');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Angle Pin','Angle-Pin','6381~6385');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Guide','Slider-Guide','6386~6390');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Slider Misc','Slider-Misc','6391~6399');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Cooling Layout','Cooling-Layout','7000');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Ejector Pin','Ejector-Pin','S001~S030');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Ejector Sleeve','Ejector-Sleeve','S002');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Spring','Spring','S031~S040');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Return Spring','RP-Spring1','S041');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('EGB','EGB','S042');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('EGP','EGP','S043');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('STP','STP','S044');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Bush','Guide-Bush','S045~S047');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Guide Pin','Guide-Pin','S048~S050');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Limt Switch','Limt-Switch','S081~S085');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Screw(FHCS,LHCS)','MSHCS','S091~S140');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Nipple,Extension-Nipple,Sealing-Plug,O-Ring','Nipple','S141~S160');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Other Standard Part','标准件','S161~S199');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Fixture','Fixture','9000~9999');
INSERT INTO [Mold].[dbo].[PartCodes] ([Name] ,[ShortName] ,[Code]) VALUES('Process Block','Process-Block','8000~8100');


------------------供应商-------------------
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('MoldBase','MoldBase',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('PUNCH','PUNCH',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('HASCO','HASCO',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('汉升','汉升 ',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('MISUMI','MISUMI',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('DME','DME',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('速利达','速利达 ',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('Warehouse','Warehouse',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('ASSAB','ASSAB',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('MoldMaster','MoldMaster',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('Husky','Husky',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('Captar','Captar',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('Special Order','Special Order',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('Uddeholm','Uddeholm',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('AISI','AISI',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('Mastip','Mastip',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('定制','定制 ',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('BOHLER','BOHLER',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('SNS','SNS',1)
INSERT INTO [Mold].[dbo].[Suppliers] ([Name] ,[FullName],[Type] ,[Enabled]) VALUES('铭振','铭振 ',1)

-------------订单状态--------------
INSERT INTO [Mold].[dbo].[PRStatus] ([PRStatusName] ,[PRStatusSequence]) VALUES ('新建' ,1);
INSERT INTO [Mold].[dbo].[PRStatus] ([PRStatusName] ,[PRStatusSequence]) VALUES ('询价中' ,2);
INSERT INTO [Mold].[dbo].[PRStatus] ([PRStatusName] ,[PRStatusSequence]) VALUES ('新建' ,1);

--------------列表值----------------
INSERT INTO LISTTYPES(NAME, ENABLED) VALUES('计划调整原因', 1)
INSERT INTO [Mold].[dbo].[ListValues] ([Name] ,[Enabled],[ListTypeID]) VALUES ('客户方设计变更' ,1,(select ListTypeID from ListTypes where Name='计划调整原因'))
INSERT INTO [Mold].[dbo].[ListValues] ([Name] ,[Enabled],[ListTypeID]) VALUES ('供应商延迟交货' ,1,(select ListTypeID from ListTypes where Name='计划调整原因'))
INSERT INTO [Mold].[dbo].[ListValues] ([Name] ,[Enabled],[ListTypeID]) VALUES ('生产部门无法按时交货' ,1,(select ListTypeID from ListTypes where Name='计划调整原因'))

----------------采购申请单自动编号----------------

INSERT INTO [Mold].[dbo].[Sequences] ([Name] ,[Current] ,[NameConvension]) VALUES ('PurchaseRequest' ,0 ,'T00000000')
INSERT INTO [Mold].[dbo].[Sequences] ([Name] ,[Current] ,[NameConvension]) VALUES ('QuotationRequest' ,0 ,'TQ0000000')
INSERT INTO [Mold].[dbo].[Sequences] ([Name] ,[Current] ,[NameConvension]) VALUES ('PurchaseOrder' ,0 ,'TO0000000')

-----------------用户岗位------------------------
INSERT [dbo].[Positions] ( [Name], [Enabled]) VALUES ( '员工', 1)
INSERT [dbo].[Positions] ( [Name], [Enabled]) VALUES ( '主管', 1)
INSERT [dbo].[Positions] ( [Name], [Enabled]) VALUES ( '经理', 1)