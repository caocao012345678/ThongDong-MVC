--USE MASTER
--GO
--IF EXISTS(SELECT * FROM SYSDATABASES WHERE NAME= 'ThongDongMVC')
--   DROP DATABASE ThongDongMVC
--GO
--CREATE DATABASE ThongDongMVC
--GO

USE ThongDongMVC
GO

DROP TABLE USER_ROLES_MAPPING
DROP TABLE ROLE_MASTER
DROP TABLE FEEDBACK
DROP TABLE BOOKING_DETAIL
DROP TABLE BOOKING_DETAIL_PEOPLE
DROP TABLE TOUR
DROP TABLE ACCOUNT
DROP TABLE BOOKING
DROP TABLE GUIDE
DROP TABLE DESTINATION

CREATE TABLE DESTINATION (
    DESTINATION_ID INT PRIMARY KEY,
    NAME NVARCHAR(255)	
);

CREATE TABLE GUIDE (
    GUIDE_ID INT PRIMARY KEY,
    NAME NVARCHAR(255),
    DESCRIPTION NTEXT,
	HTML VARCHAR(255)
);

CREATE TABLE BOOKING (
    BOOKING_ID INT PRIMARY KEY,    
	QUANTITY_ADULT INT,
	QUANTITY_CHILD INT,
	ORDER_DATE DATETIME,
	STATUS NVARCHAR(255),
);

CREATE TABLE ACCOUNT (
    ACCOUNT_ID INT PRIMARY KEY IDENTITY(1,1),
    USERNAME VARCHAR(50),
    PASSWORD VARCHAR(255),
	PHONE VARCHAR(20),
	EMAIL VARCHAR(255),
    ACCOUNT_TYPE NVARCHAR(20)
);

CREATE TABLE TOUR (
    TOUR_ID INT PRIMARY KEY,
    NAME NVARCHAR(255),
    DESTINATION_ID INT,
    START_DATE DATE,
    END_DATE INT,
    PRICE_CHILD INT,
    PRICE_ADULT INT,
    DESCRIPTION NTEXT,
	HTML VARCHAR(255)
	FOREIGN KEY (DESTINATION_ID) REFERENCES DESTINATION(DESTINATION_ID)
);

CREATE TABLE BOOKING_DETAIL_PEOPLE(
	BOOKING_DETAIL_PEOPLE_ID INT PRIMARY KEY IDENTITY(1,1),
	BOOKING_ID INT,
	NAME NVARCHAR(255),
	PHONE VARCHAR(255),
	EMAIL VARCHAR(255)	
	FOREIGN KEY (BOOKING_ID) REFERENCES BOOKING(BOOKING_ID)
);

CREATE TABLE BOOKING_DETAIL(
	BOOKING_DETAIL_ID INT PRIMARY KEY,
	BOOKING_ID INT,
	TOUR_ID INT,
	BOOKING_PEOPLE INT,
    ACCOUNT_ID INT,
	START_DATE DATE,
    END_DATE DATE,
	PRICE_CHILD INT,
    PRICE_ADULT INT,
	QUANTITY_ADULT INT,
	QUANTITY_CHILD INT,
	OTHER_REQUIREMENTS NTEXT,
	FOREIGN KEY (BOOKING_ID) REFERENCES BOOKING(BOOKING_ID),
	FOREIGN KEY (TOUR_ID) REFERENCES TOUR(TOUR_ID),
	FOREIGN KEY (ACCOUNT_ID) REFERENCES ACCOUNT(ACCOUNT_ID),
);

CREATE TABLE FEEDBACK (
    FEEDBACK_ID INT PRIMARY KEY IDENTITY(1,1),
    ACCOUNT_ID INT,
	NAME NVARCHAR(255),
	EMAIL VARCHAR(255),
    FEEDBACK_DATE DATE,
    FEEDBACK_TEXT NTEXT,
    FOREIGN KEY (ACCOUNT_ID) REFERENCES ACCOUNT(ACCOUNT_ID),
);

CREATE TABLE ROLE_MASTER(
	ID INT PRIMARY KEY IDENTITY(1,1),
	RollName VARCHAR(50)
);

CREATE TABLE USER_ROLES_MAPPING(
	ID INT PRIMARY KEY,
	UserID INT NOT NULL,
	RoleID INT NOT NULL,
	FOREIGN KEY (UserID) REFERENCES ACCOUNT(ACCOUNT_ID),
	FOREIGN KEY (RoleID) REFERENCES ROLE_MASTER(ID)
);

GO

INSERT INTO ACCOUNT(USERNAME,PASSWORD,ACCOUNT_TYPE,PHONE,EMAIL)
VALUES	('admin','21232f297a57a5a743894a0e4a801fc3','ADMIN',0123456789,'2200001536@nttu.edu.vn'),
		('user', 'ee11cbb19052e40b07aac0ca060c23ee','USER',0123456789,'2200001995@nttu.edu.vn')

GO

INSERT INTO ROLE_MASTER(RollName)
VALUES	('ADMIN'),
		('USER')

GO


INSERT INTO DESTINATION(NAME,DESTINATION_ID)
VALUES	(N'Sapa',01),
		(N'Hà Nội',02),
		(N'Đà Nẵng',03),
		(N'Hồ Chí Minh',04),
		(N'Nha Trang',05),
		(N'Quy Nhơn',06),
		(N'Tây Nguyên',07),
		(N'Phú Quốc',08),
		(N'Cần Thơ',09),
		(N'Đà Lạt',10),
		(N'Phan Thiết',11)

GO

INSERT INTO GUIDE(GUIDE_ID, NAME, DESCRIPTION, HTML)
VALUES(01,N'Sapa',N'Nằm dưới chân dãy Hoàng Liên Sơn hùng vĩ và chỉ cách Hà Nội 5 - 6 giờ di chuyển bằng đường cao tốc, Sa Pa là điểm đến lý tưởng quanh năm. Ngoài những thửa ruộng bậc thang vào mùa lúa chín, Sa Pa còn hút khách nhờ vẻ đẹp của nhiều điểm đến khác.','~/Views/ThongDong/guideSapa.cshtml'),
	  (02,N'Phú Quốc',N'Quần đảo Phú Quốc nằm trong vịnh Thái Lan, cách TP HCM khoảng 400 km về hướng tây. Nơi đây thu hút du khách trong và ngoài nước bởi các loại hình du lịch đa dạng, với tài nguyên biển, đảo phong phú; hệ sinh thái rừng, biển đa dạng.
Vùng biển Phú Quốc có 22 hòn đảo lớn, nhỏ, tổng diện tích khoảng 589,23 km2. Trong đó, đảo Phú Quốc lớn nhất được chia thành bắc đảo và nam đảo. Thị trấn Dương Đông nằm ở trung tâm.','~/Views/ThongDong/guidePhuQuoc.cshtml'),
	  (03,N'Nha Trang',N'Ngoài vui chơi trên biển, du khách có thể tham quan đảo khỉ, tháp bà Ponagar và thưởng thức món ngon của miền biển. Nha Trang, thành phố biển nằm tại vị trí trung tâm tỉnh Khánh Hòa, từ lâu đã là một trong những điểm du lịch hút khách nhất Việt Nam. Được thiên nhiên ban tặng những bãi biển, vùng vịnh đẹp cùng nhiều đảo lớn nhỏ, thành phố còn sở hữu nhiều danh lam thắng cảnh, điểm tham quan, vui chơi và cơ sở nghỉ dưỡng.','~/Views/ThongDong/guideNhaTrang.cshtml'),
	  (04,N'Đà Nẵng',N'Thành phố Đà Nẵng nằm ở miền Trung, chia đều khoảng cách giữa thủ đô Hà Nội và TP HCM. Đà Nẵng còn là trung tâm của 3 di sản văn hóa thế giới là Cố đô Huế, phố cổ Hội An và thánh địa Mỹ Sơn. Phía bắc Đà Nẵng giáp tỉnh Thừa Thiên - Huế, phía tây và nam giáp tỉnh Quảng Nam, phía đông giáp biển Đông.','~/Views/ThongDong/guideDaNang.cshtml'),
	  (05,N'Lăng Cô',N'Thị trấn Lăng Cô cách thành phố Huế hơn 60 km và cách Đà Nẵng khoảng 25 km thuộc huyện Phú Lộc. Nằm lọt thỏm giữa nhánh của dãy Trường Sơn đâm ra biển, một bên là đèo Hải Vân, một bên là đèo Phú Gia, gần cảng Chân Mây, Lăng Cô hiện lên là một dãy cồn cát đẹp và chạy dài. Khu vực Lăng Cô còn có những cánh rừng nhiệt đới.','~/Views/ThongDong/guideLangCo.cshtml'),
	  (06,N'Đà Lạt',N'Đà Lạt là thủ phủ tỉnh Lâm Đồng, nằm trên cao nguyên Lâm Viên ở độ cao 1.500 m so với mực nước biển. Cách TP HCM khoảng 300 km và có khí hậu mát mẻ năm nên Đà Lạt là điểm nghỉ dưỡng lý tưởng, giúp du khách thoát khỏi nóng nực của vùng Nam Bộ.','~/Views/ThongDong/guideDaLat.cshtml'),
	  (07,N'Vũng Tàu',N'Vũng Tàu là thành phố biển thuộc tỉnh Bà Rịa - Vũng Tàu, ở vùng Đông Nam Bộ. Nếu nhìn theo chiều Bắc Nam, Vũng Tàu nằm ở khúc quanh đang đổi hướng từ Nam sang Tây của phần dưới chữ S trên bản đồ Việt Nam và nhô hẳn ra khỏi đất liền.','~/Views/ThongDong/guideVungTau.cshtml')

GO

INSERT INTO TOUR (TOUR_ID, NAME, DESTINATION_ID, START_DATE, END_DATE, PRICE_CHILD, PRICE_ADULT, DESCRIPTION, HTML)
VALUES	(01,N'Tour Sapa:Trekking Bản Làng',01,'2023-11-10',2,69,99,N'Sa Pa là một thị trấn vùng cao, ẩn chứa nhiều điều kỳ diệu của tự nhiên, phong cảnh thiên nhiên thiên nhiên với địa hình của núi đồi, tạo nên bức tranh có bố cục hài hoà, có cảnh sắc thơ mộng và hấp dẫn từ cảnh quan đất trời vùng đất phía Tây Bắc mà quý khách có thể trải nghiệm trong chuyến trekking bản Má Tra.Ngoài ra, du khách có thể chinh phục đỉnh Fansipan của dãy Hoàng Liên Sơn, ngắm vườn hoa Hàm Rồng và khám phá thung lũng Mường Hoa.
Cùng iVIVU khám phá vùng đất diệu kỳ này ngay hôm nay!','~/Views/ThongDong/tour_Sapa2N1D.cshtml'),
		(02,N'Tour Miền Bắc 4N3Đ: Hà Nội - Ninh Bình - Vịnh Hạ Long',02,'2023-11-10',4,109,149,N'Xứ Bắc - vùng đất khai cơ của các Vương triều Việt, nơi định đô của hầu hết các triều đại phong kiến Việt Nam chính vì vậy nơi đây được xem như là đất kinh đô ngàn năm văn hiến với một bề dày văn hóa, lịch sử sâu sắc và đa dạng.
Tuy nhiên, khi đến đây, Du khách không chỉ được tham quan những công trình văn hóa - lịch sử cổ kính như: Văn Miếu, Hoàng thành Thăng Long, Cố đô Hoa Lư, Chùa Bái Đính, trải nghiệm những nét văn hóa đặc sắc của đất kinh đô như : ngoạn cảnh 36 phố phường, thưởng thức ẩm thực, xem múa rối nước; mà còn có thể thăm thú các thắng cảnh nổi tiếng nơi đây như: Di sản thế giới Danh thắng Tràng An và Vịnh Hạ Long.
Cùng iVIVU trải ngiệm vùng đất tuyệt vời này ngay hôm nay!','~/Views/ThongDong/tour_HN_NB_VHL.cshtml'),
		(03,N'Tour Đà Nẵng 5N4Đ: Hội An - Bà Nà - Huế - Động Phong Nha',03,'2023-11-10',5,149,199,N'Miền Trung, dải đất thân thương nối liền hai miền Nam - Bắc của Việt Nam. “Cung đường di sản miền Trung” sẽ kết nối du khách với các di sản văn hóa của nhân loại được UNESCO công nhận bao gồm: Quảng Bình cùng Vườn quốc gia Phong Nha - Kẻ Bàng; Huế với hai di sản là Quần thể di tích Cố đô Huế và Nhã Nhạc cung đình; Quảng Nam với hai di sản là Thánh địa Mỹ Sơn và đô thị cổ Hội An. Trên mỗi bước đi, du khách còn có thể khám phá cho mình nhiều điểm đến hấp dẫn: Bà Nà Hill, Làng Chài Lăng Cô, …và nhiều bãi tắm tuyệt đẹp: Cửa Đại, Non Nước, Lăng Cô, Nhật Lệ…cùng với những nét ẩm thực độc đáo của Miền Trung.
Cùng iVIVU trải nghiệm cung đường tuyệt vời này ngay hôm nay!','~/Views/ThongDong/tour_DaNang5N4D.cshtml'),
		(04,N'Tour Xe Lửa Miền Trung 3N4Đ: HCM - Quy Nhơn - Lý Sơn',04,'2023-11-10',4,119,159,N'Không hiện đại, tốc độ nhanh hay tiện nghi như tàu điện cao tốc, Xe Lửa (Tàu Hỏa) lại mang dấu ấn rất riêng, là một phương tiện di chuyển đã gắn bó và thân thương với dân ta biết bao thế hệ. Những đoàn tàu với tiếng còi quen thuộc di chuyển chầm chậm qua những làng quê, ga tàu yên ả, cảnh sắc thiên nhiên Việt Nam tuyệt đẹp có thể là một trải nghiệm rất khác biệt - một hành trình của những hoài niệm và các cung bậc cảm xúc.
Quy Nhơn vẫn là điểm đến yêu thích của “hội mê đi biển”. Được tạo hoá ưu ái ban tặng thiên nhiên trù phú, Quy Nhơn sở hữu vô số bãi biển, ghềnh đá, hòn đảo đẹp, còn vẹn nguyên vẻ đẹp hoang sơ và thanh bình.
Đến với Lý Sơn, du khách sẽ được tìm hiểu về Hải đội Hoàng Sa anh hùng, thỏa sức bơi lội trong làn nước trong mát của Đảo Bé, thưởng thức các món hải sản được người dân nơi đây đánh bắt về. Cùng iVIVU khám phá Quy Nhơn - Lý Sơn ngay hôm nay !','~/Views/ThongDong/tour_HCM_QN_LS.cshtml'),
		(05,N'Tour Cao Cấp Nha Trang 3N2Đ: Vinwonders - Du Ngoạn Vịnh Đảo - City Tour',05,'2023-11-10',3,189,239,N'Thành phố Nha Trang đẹp như một bức tuyệt tác của tự nhiên và con người với nhiều địa điểm du lịch nổi tiếng. Cảnh quan thiên nhiên đa dạng đã làm nên sức hút khó cưỡng của những hoạt động du lịch nơi đây, cùng với nền ẩm thực phong phú và con người thân thiện – nơi đây như đã chiếm trọn trái tim du khách muôn phương. Hứa hẹn sẽ mang đến cho bạn một chuyến nghỉ dưỡng đáng nhớ. Cùng iVIVU khám phá mảnh đất diệu kỳ này ngay hôm nay!','~/Views/ThongDong/tour_VW_DNVD_CT.cshtml'),
		(06,N'Tour Miền Trung 3N2Đ: Quy Nhơn - Phú Yên',06,'2023-11-10',3,179,229,N'Xứ Nẫu được thiên nhiên ban tặng phong cảnh nên thơ, hữu tình và những bãi biển trải dài tuyệt đẹp, Phú Yên - Bình Định trở thành điểm nổi bật cho chuyến du lịch hè đáng nhớ cho du khách mọi miền. Không chỉ mang nét duyên của thành phố ôm trọn hai ngọn núi, Phú Yên còn mê đắm lòng người ở những đầm nước trong vắt, những bãi biển bao la, ghềnh Đá Đĩa kỳ thú. Qua đến Quy Nhơn trải nghiệm những hành trình hấp dẫn như Kỳ Co nước trong xanh màu ngọc bích được mệnh danh là “Maldives của Việt Nam”, Eo Gió vừa lãng mạn vừa hùng vĩ để ngắm hoàng hôn. Xứ Nẫu nổi lên kiến trúc Champa Tháp Nhạn, Tháp Đôi độc đáo được xây dựng khoảng thế kỷ 12.','~/Views/ThongDong/tour_QN_PY.cshtml'),
		(07,N'Tour Tây Nguyên 3N2Đ: Măng Đen - Ngã 3 Đông Dương - Khẩu Bờ Y - Gia Lai',07,'2023-11-10',3,209,239,N'Khu du lịch sinh thái Măng Đen được du khách xa gần biết đến như “Đà Lạt thứ hai” của Tây Nguyên. Măng Đen vẫn giữ nguyên vẻ đẹp hoang sơ hiếm có của đại ngàn Tây Nguyên với rừng, suối, hồ và thác nước. Măng Đen e ấp ẩn mình bên cánh rừng nguyên sinh với những rặng thông đỏ bạt ngàn trải dài như vô tận, xen lẫn tiếng nước chảy róc rách quanh năm...
Cảm giác thú vị bất ngờ xâm chiếm sau hành trình dài giữa đại ngàn hùng vĩ, du khách thấy Măng Đen bốn phía được bao bọc bởi sương mù, rừng thông, biệt thự và cái lạnh nhè nhẹ... Ngoài bạt ngàn cây xanh, ở Măng Đen thỉnh thoảng khách bộ hành còn bắt gặp nhiều suối, thác đẹp nổi tiếng như: Pa Sỹ, Đăk Ke, Lô Ba và những hồ thơ mộng: Toong Zơri, Toong Pô, Toong Đam. Là một thị trấn thuộc huyện Kon Plong, tỉnh Kon Tum, Măng Đen được tự nhiên ưu ái khi mang trong mình đến tận bảy hồ, ba thác với dòng nước xanh trong ngọt ngào quanh năm. Cùng iVIVU khám phá ngay hôm nay!','~/Views/ThongDong/tour_MD_N3DD_KBY.cshtml'),
		(08,N'Tour Miền Trung 3N2Đ: HCM - Kỳ Co - Eo Gió - Phú Yên',04,'2023-11-10',3,249,299,N'Quy Nhơn – Thành phố trung tâm của Tỉnh Bình Định với bờ biển dài, chạy quanh thành phố, được đánh giá là một trong những bãi biển đẹp nhất Miền Trung. Với thành Đồ Bàn – Kinh đô cũ của đất nước Champa vẫn còn đó những nét vết tích cổ kính, rêu phong.
Nằm cách Tp Quy Nhơn khoảng 100km về phía nam, Tp Tuy Hòa vẫn khá mới lạ và còn nhiều nét hoang sơ trong bản đồ du lịch của nhiều du khách. Đến Tuy Hòa, quý khách nhất định phải ghé đến Gành Đá Đĩa – Thắng cảnh độc nhất vô nhị của Việt Nam. Cùng iVIVU khám phá 2 tỉnh ngay hôm nay !','~/Views/ThongDong/tour_HCM_KC_EG_PY.cshtml'),
		(09,N'Tour Phú Quốc 3N2Đ: HCM - Grand World - Câu Cá - Lặn Ngắm San Hô',08,'2023-11-10',3,229,239,N'Thường được biết đến với tên Đảo Ngọc, Phú Quốc sở hữu những bãi biển hoang sơ tuyệt đẹp, sinh vật biển đa dạng và cảnh quan thiên nhiên phong phú được kết hợp tài tình với những cánh rừng tự nhiên, núi, biển và thác nước, sông hồ. Phú Quốc luôn là một trong những điểm nghĩ dưỡng hàng đầu với các khu resort đẳng cấp quốc tế cùng với những khu vui chơi, giải trí quy mô bậc nhất Đông Nam Á như công viên chủ đề Vin Wonders, thành phố không ngủ Grand World, casino Phú Quốc....Ngoài ra, Phú Quốc vẫn còn lưu giữ nhiều di tích văn hóa, lịch sử và các nghề truyền thống hàng trăm năm tuổi như nghề làm nước mắm Phú Quốc trứ danh.','~/Views/ThongDong/tour_HCM_GW_CC.cshtml'),
		(10,N'Tour Limousine Miền Tây 3N3Đ: Du Thuyền Cần Thơ - Cực Nam - Mũi Cà Mau - Ăn Cua Biển',09,'2023-11-10',4,79,89,N'Xe Limousine là dòng xe hiện đại, tối ưu và sang trọng, được áp dụng phục vụ cho hành khách tuyến đường dài để đảm bảo sự nghỉ ngơi và thoải mái khi đi trên xe. Để chuyến du lịch được trọn vẹn và giữ gìn được sức khỏe cho quý khách thì đây là giải pháp cho những trải nghiệm thật nhẹ nhàng và thoải mái nhất…
Được thiên nhiên ưu đãi cho yếu tố địa hình sông nước với những vườn trái cây trĩu quả, những cánh đồng lúa thẳng cánh cò bay, vì thế nên miền Tây luôn là điểm đến lý tưởng để du khách hòa mình vào thiên nhiên, sau những ngày làm việc bận rộn nơi phố thị ồn ào. Mỗi tỉnh miền Tây có một màu sắc riêng, mỗi đặc trưng riêng, cùng iVIVU khám phá ngay hôm nay!','~/Views/ThongDong/tour_CT_CN_MCM.cshtml'),
		(11,N'Tour Đà Lạt 4N3Đ : Đà Lạt - Nha Trang - Thành Phố Hoa Biển',10,'2023-11-10',4,299,339,N'Du lịch Nha Trang, Đà Lạt là một trong những lịch trình được nhiều người quan tâm vì đây là hai trong số nhiều địa danh thu hút đông đảo du khách. Nếu Đà Lạt mang vẻ đẹp kiêu sa của một thiếu nữ rực rỡ trong sắc xuân thì Nha Trang – nơi được mệnh danh là “hòn ngọc của Biển Đông” lại mang vẻ đẹp lung linh, huyền ảo với biển xanh, cát trắng cùng những hàng dừa rì rào, thẳng tắp. Du khách vừa được đắm mình trong khí trời se se lạnh của thành phố sương mù lại hòa mình cùng sóng biển cát vàng xứ Trầm hương . Giờ đây, hãy gác lại mọi bồn bề của cuộc sống để cùng iVIVU khám phá lịch trình hấp dẫn này ngay hôm nay!','~/Views/ThongDong/tour_DL_NT_TPHB.cshtml'),
		(12,N'Tour Limousine Phan Thiết 3N2Đ: Hồ Tràm - Phan Thiết - Mũi Né - Công Viên Tropicana',11,'2023-11-10',3,289,319,N'Xe Limousine hay được gọi là “Chuyên Cơ Mặt Đất“Xe Limousine là dòng xe hiện đại, tối ưu và sang trọng, được cải tiến từ những chiếc xe du lịch 40 chỗ thành những chiếc xe 20 chỗ sang trọng, xe du lịch 29 chỗ thành 18-20 chỗ. Xe Limousine được áp dụng phục vụ cho hành khách tuyến đường dài để đảm bảo sự nghỉ ngơi và thoải mái khi đi trên xe.
Thiên nhiên dường như quá ưu đãi cho bờ biển Phan Thiết, bởi nó chứa đựng những điều cuốn hút du khách, biển xanh, cát trắng, rặng dừa xanh,…đã tạo cho Phan Thiết sức hấp dẫn khó cưỡng cho những ai yêu biển, yêu cái nắng vàng trên cát. Ngỡ như khô cằn nhưng lại vô cùng lãng mạn. Đừng nghĩ Phan Thiết không có gì để khám phá, thật ra nó là một điểm nghĩ dưỡng sau những ngày làm mệt mỏi, căng thẳng nơi phố thị vô cùng hữu hiệu, là nơi để bạn và gia đình thư giãn vào những ngày hè oi bức, gắn kết tình thân hay là nơi trăng mật vô cùng lãng mạn.','~/Views/ThongDong/tour_HT_PT_MN.cshtml'),
		(13,N'Tour Limousine Cao Cấp Phan Thiết 2N1Đ: Nghỉ Dưỡng Centara Mirage Resort - Bánh Canh Tôm Hùm',11,'2023-11-10',2,349,399,N'Xe Limousine hay được gọi là “Chuyên Cơ Mặt Đất” dòng xe hiện đại, tối ưu và sang trọng, được cải tiến từ những chiếc xe du lịch 40 chỗ thành những chiếc xe 20 chỗ sang trọng, xe du lịch 29 chỗ thành 18-20 chỗ. Xe Limousine được áp dụng phục vụ cho hành khách tuyến đường dài để đảm bảo sự nghỉ ngơi và thoải mái khi đi trên xe. Cả nội thất bên trong của các xe đều được lắp đặt các thiết bị hiện đại và tiện ích nhất cho các trải nghiệm của hành khách.
Thiên nhiên dường như quá ưu đãi cho bờ biển Phan Thiết, bởi nó chứa đựng những điều cuốn hút du khách, biển xanh, cát trắng, rặng dừa xanh,…đã tạo cho Phan Thiết sức hấp dẫn khó cưỡng cho những ai yêu biển, yêu cái nắng vàng trên cát. Ngỡ như khô cằn nhưng lại vô cùng lãng mạn. Đừng nghĩ Phan Thiết không có gì để khám phá, thật ra nó là một điểm nghĩ dưỡng sau những ngày làm mệt mỏi, căng thẳng nơi phố thị vô cùng hữu hiệu, là nơi để bạn và gia đình thư giãn vào những ngày hè oi bức, gắn kết tình thân hay là nơi trăng mật vô cùng lãng mạn.','~/Views/ThongDong/tour_PT_NDCMR.cshtml');


