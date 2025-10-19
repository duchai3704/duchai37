create database QLDienThoaiPDH_64130531
go
use QLDienThoaiPDH_64130531
go

-- Table: User
CREATE TABLE Users (
    taikhoan NVARCHAR(20) PRIMARY KEY,
    matkhau NVARCHAR(20) NOT NULL,
    ten NVARCHAR(50),
    sdt CHAR(10),
    email NVARCHAR(50),
    diaChi NVARCHAR(100)
);
-- Table: Hangsanxuat
CREATE TABLE Hangsanxuat (
    Mahang INT PRIMARY KEY,
    Tenhang NVARCHAR(50)
);

-- Table: Hedieuhanh
CREATE TABLE Hedieuhanh (
    Mahdh INT PRIMARY KEY,
    Tenhdh NVARCHAR(50)
);

-- Table: Khuyenmai
CREATE TABLE Khuyenmai (
    maKhuyenmai Char(10) PRIMARY KEY,
    tenKhuyenmai NVARCHAR(50),
    phanTram FLOAT
);
-- Table: Color
CREATE TABLE Color (
    maMau INT PRIMARY KEY,
    tenMau NVARCHAR(50)
);

-- Table: BoNhoTrong
CREATE TABLE BoNhoTrong (
    id_dungluong INT PRIMARY KEY,
    Dungluong VARCHAR(50) NOT NULL
);


-- Table: Trangthai
CREATE TABLE Trangthai (
    id INT PRIMARY KEY,
    tenTrangthai NVARCHAR(50)
);


go

-- Table: Blog
CREATE TABLE Blog (
    maBlog CHAR(10) PRIMARY KEY,
    tieude NVARCHAR(100),
    noidung NVARCHAR(MAX),
    ngaytao DATE default GETDATE(),
    hinhanh NVARCHAR(100),
	Mahang INT not null,
	FOREIGN KEY ( Mahang) REFERENCES Hangsanxuat( Mahang)
);


-- Table: Sanpham
CREATE TABLE Sanpham (
    maSanpham CHAR(10) PRIMARY KEY,
    tenSanpham NVARCHAR(100),
    moTa NVARCHAR(MAX),
    soLuongyeuthich INT not null default 0,
    Mahdh INT not null,
    Mahang INT not null,
	maKhuyenmai CHAR(10) not null,
    FOREIGN KEY (Mahdh) REFERENCES Hedieuhanh(Mahdh),
    FOREIGN KEY (Mahang) REFERENCES Hangsanxuat(Mahang),
	FOREIGN KEY ( maKhuyenmai) REFERENCES Khuyenmai( maKhuyenmai)
);
go 

-- Table: Chitietsanpham
CREATE TABLE Chitietsanpham (
    id_chitietSP CHAR(10) PRIMARY KEY,
    hinhAnh NVARCHAR(100),
    soLuongTon INT not null default 0,
    gia DECIMAL(10, 2),
	maMau INT not null,
    maSanpham CHAR(10) not null,
    id_dungluong INT not null ,
    FOREIGN KEY (maSanpham) REFERENCES Sanpham(maSanpham),
    FOREIGN KEY (maMau) REFERENCES Color(maMau),
    FOREIGN KEY (id_dungluong) REFERENCES BoNhoTrong(id_dungluong)
);


-- Table: Comment
CREATE TABLE Comment (
    maComment CHAR(10) PRIMARY KEY,
    noiDung TEXT,
    ngayTao DATE default GETDATE(),
	ma_cmCha char(10) , -- Nếu trả lời bình luận nào khác thì cập nhật id đó vào đây
    taikhoan NVARCHAR(20) not null,
    maBlog CHAR(10) not null,
    FOREIGN KEY (taikhoan) REFERENCES Users(taikhoan),
    FOREIGN KEY (maBlog) REFERENCES Blog(maBlog)
);

-- Table: Review
CREATE TABLE Review (
    maReview CHAR(10) PRIMARY KEY,
    noiDung TEXT,
    ngayNhap DATE default GETDATE(),
    soSao INT not null default 1,
    taikhoan NVARCHAR(20) not null,
    maSanpham CHAR(10) not null,
    FOREIGN KEY (taikhoan) REFERENCES Users(taikhoan),
    FOREIGN KEY (maSanpham) REFERENCES Sanpham(maSanpham)
);

-- Table: Hoadon
CREATE TABLE Hoadon (
    maHoadon CHAR(10) PRIMARY KEY,
    diaChi NVARCHAR(100),
    ngayTao DATE  default GETDATE(),
    ngayGiao DATE,
    id_Trangthai INT not null,
    tongTien DECIMAL(10, 2),
	taikhoan NVARCHAR(20) not null,
    FOREIGN KEY (id_Trangthai) REFERENCES Trangthai(id),
	FOREIGN KEY (taikhoan) REFERENCES Users(taikhoan),
	 CHECK (ngayGiao > ngayTao) 
);

go
-- Table: Chitiethoadon
CREATE TABLE Chitiethoadon (
	id_chitietHD CHAR(10)  PRIMARY KEY,
    maHoadon CHAR(10),
    id_chitietSP CHAR(10),
    soLuong INT,
    FOREIGN KEY (maHoadon) REFERENCES Hoadon(maHoadon),
    FOREIGN KEY (id_chitietSP) REFERENCES Chitietsanpham(id_chitietSP)
);

go
ALTER TABLE Chitiethoadon
ADD trangThai int;



insert into Hangsanxuat values(1,N'APPLE'),
	(2,N'SAMSUNG'),
    (3,N'XIAOMI'),
	(4,N'TECNO'),
	(5,N'OPPO')

select * from BoNhoTrong
delete from Color

insert into color values(1,N'Xanh'),
							(2,N'Trắng'),
							(3,N'Vàng'),
							(4,N'Tím'),
							(5,N'Đen'),
							(6,N'Xám'),
							(7,N'Hồng')

INSERT INTO BoNhoTrong
VALUES 
(1, '64'),
(2, '128'),
(3, '256'),
(4, '512'),
(5, '1000')


insert into Hedieuhanh values (1,N'ISO'),
(2,N'ANDROID'),
(3,N'WINDOWS PHONE')

select * from Hedieuhanh
delete from Hedieuhanh

INSERT INTO Sanpham (maSanpham, tenSanpham, moTa, Mahdh, Mahang, maKhuyenmai) 
VALUES 
('SP000001', N'iPhone', N'Apple vừa ra mắt iPhone 16 Pro Max (256GB) mới và mang đến cho các tín đồ iPhone một mẫu điện thoại được cải tiến nhiều nhất trong bốn mẫu mà thương hiệu này đã giới thiệu tại sự kiện Glowtime.', 1, 1, 'KM0001'),
('SP000002', N'Samsung Galaxy Z Fold',N'Samsung Galaxy Z Fold6 là dòng điện thoại thông minh thế hệ Galaxy Z Fold tiếp theo được Samsung công bố ra thị trường với nhiều tính năng và cập nhật lớn.', 2, 2, 'KM0002'),
('SP000003', N'Xiaomi Redmi',N'Xiaomi Redmi 12 4GB/128GB là chiếc điện thoại thuộc phân khúc tầm trung, với giá cả phải chăng và hiệu suất chất lượng với bộ thông số ấn tượng.', 2, 3, 'KM0002'),
('SP000004', N'Redmi Note Pro 5G',N'Tiếp nối thành công từ những thế hệ trước, thương hiệu tiếp tục cho ra mắt Redmi Note 13 Pro+ 5G (8GB/256GB) với nhiều thiết kế lần đầu tiên xuất hiện.', 2, 3, 'KM0002'),
('SP000005', N'TECNO SPARK 10',N'TECNO SPARK 10 thuộc điện thoại phân khúc giá rẻ của TECNO nhưng sở hữu nhiều ưu điểm vượt trội.', 2, 4, 'KM0002'),
('SP000006', N'Samsung Galaxy Ultra',N'Điện thoại Samsung Galaxy S24 Ultra - Chính hãng sở hữu màn hình 6.8 inch với độ phân giải QHD+, sử dụng công nghệ màn hình Dynamic AMOLED 2X.', 2, 2, 'KM0002'),
('SP000007', N'TECNO Spark', N'Điện thoại TECNO SPARK 30C được thiết kế với cấu hình mạnh mẽ, tích hợp nhiều tính năng tiên tiến nhằm đáp ứng nhu cầu sử dụng đa dạng của người dùng, từ công việc, học tập cho đến giải trí.', 2, 4, 'KM0002'),
('SP000008', N'iPhone Pro Max', N'iPhone 15 Pro Max đã chính thức được ra mắt trong sự kiện Wonderlust tại nhà hát Steve Jobs, California (Mỹ) vào lúc 10h sáng 12/9 tức 0h ngày 13/9 (giờ Việt Nam).', 1, 1, 'KM0002'),
('SP000009', N'OPPO Find Flip', N'Chiếc điện thoại này sở hữu loạt thông số ấn tượng, từ màn hình chính LTPO AMOLED Full HD+ 120Hz sắc nét đến bộ vi xử lý MediaTek Dimensity 9200 mạnh mẽ. Bên cạnh đó, thế hệ N3 được cải tiến thiết kế, mang đến vẻ ngoài khác biệt so với thế hệ trước.', 2, 5, 'KM0002'),
('SP000010', N'OPPO Find', N'Được nâng cấp và phát triển từ thế hệ trước OPPO Find N2, sản phẩm smartphone mới này sở hữu cấu hình và tính năng mạnh mẽ ấn tượng.', 2, 5, 'KM0002');


select * from Sanpham
delete from Sanpham

INSERT INTO Khuyenmai (maKhuyenmai, tenKhuyenmai, phanTram) 
VALUES 
('KM0001', N'Khuyến mãi mùa hè', 10.0), -- 10% giảm giá
('KM0002', N'Black Friday Sale', 20.0), -- 20% giảm giá
('KM0003', N'Giảm giá cuối năm', 15.0), -- 15% giảm giá
('KM0004', N'Ưu đãi khách hàng VIP', 25.0), -- 25% giảm giá
('KM0005', N'Mua 1 tặng 1', 50.0); -- Tương đương giảm giá 50%


INSERT INTO Khuyenmai (maKhuyenmai, tenKhuyenmai, phanTram) 
VALUES ('KM0000', 'Không khuến mãi', 0)

INSERT INTO Chitietsanpham (id_chitietSP, hinhAnh, soLuongTon, gia, maMau, maSanpham, id_dungluong, Dungluong, tenMau)
VALUES 
('0000010140', 'iphone-16-pro-sa-mac-1.png', 100, 32990.00, 3, 'SP000001', 5, '1000', N'Vàng'),
('0000010141', 'ip16-xanh-luu-ly.png', 200, 24890.00, 1, 'SP000001', 4, '512', N'Xanh'), 
('0000010242', 'remi12-a.png', 150, 2990.00, 5, 'SP000003', 2, '128', N'Đen'),
('0000020140', 'ip16-xanh-mong-ket.png', 100, 21890.00, 1, 'SP000001', 3, '256', N'Xanh'),
('0000020141', 'z-fold6-thumb.png', 200, 45990.00, 6, 'SP000002', 4, '512', N'Xám'),
('0000020242', 'iphone-15-pro-finish.png', 150, 34390.00, 6, 'SP000001', 4, '512', N'Xám'),
('0000030140', 'oppo-find-n3-flip.png', 100, 19990.00, 3, 'SP000010', 4, '512', N'Vàng'),
('0000030141', 'oppo-find-n3-ad.png', 200, 39790.00, 3, 'SP000010', 5, '1000', N'Vàng'),
('0000040140', 'z-flip6-thumb.png', 50, 28990.00, 1, 'SP000002', 4, '512', N'Xanh'),
('0000040141', 'ecno-spark-30c-3.png', 75, 2490.00, 1, 'SP000005', 2, '128', N'Xanh'),
('0000040242', 'samsung-galaxy-a06-3-christmas.png', 120, 3490.00, 2, 'SP000002', 1, '64', N'Trắng'),
('0000050140', 's24-ultra-vang.png', 80, 33490.00, 3, 'SP000002', 4, '512', N'Vàng'),
('0000050141', 'redmi-note-13-christmas.png', 150, 3790.00, 3, 'SP000003', 2, '128', N'Vàng'),
('0000050242', 'xiaomi-14.png', 90, 18490.00, 1, 'SP000004', 3, '256', N'Xanh'),
('0000060140', 'xiaomi-14t.png', 70, 14990.00, 6, 'SP000003', 2, '128', N'Xám'),
('0000060141', 'tecno-spark-go-1-3.png', 130, 2190.00, 5, 'SP000005', 1, '64', N'Đen'),
('0000060242', 'note-11-pro-5g-2.png', 100, 5590.00, 1, 'SP000003', 3, '256', N'Xanh'),
('0000070140', 'pova-6-neo-xam-4.png', 60, 3990.00, 5, 'SP000007', 3, '256', N'Đen'),
('0000070141', 'image-removebg-preview.png', 90, 7890.00, 1, 'SP000009', 2, '128', N'Xanh'),
('0000070242', '14plus-removebg-preview.png', 110, 19490.00, 4, 'SP000008', 4, '512', N'Tím'),
('0000080140', 'tecno-pova-5.png', 85, 4390.00, 2, 'SP000007', 2, '128', N'Trắng'),
('0000080141', 'samsung-galaxy-s23-ultra.png', 95, 20890.00, 6, 'SP000006', 4, '512', N'Xám'),
('0000080242', 'combo-a79-black-rgb.png', 70, 6790.00, 5, 'SP000009', 3, '256', N'Đen'),
('0000090140', 'galaxy-a16-1.png', 50, 5690.00, 5, 'SP000006', 2, '128', N'Đen'),
('0000000242', 'iphone-15-pink-pure.png', 70, 21990.00, 7, 'SP000008', 3, '256', N'Hồng'),
('0000000140', 'tecno-spark-10.png', 50, 2590.00, 1, 'SP000007', 2, '128', N'Xanh');


ALTER TABLE Chitietsanpham
ADD tenMau NVARCHAR(50),
Dungluong NVARCHAR(50);


DELETE FROM Chitietsanpham
select * from Chitietsanpham


	select * from Users

	INSERT INTO Users (taikhoan, matkhau, ten, sdt, email, diaChi)
VALUES 
('phamduchai', '123', N'Trần Thị B', '0987654321', 'user456@example.com', N'456 Đường XYZ, Hà Nội'),
('user000002', '123', N'Lê Văn C', '0912345678', 'user789@example.com', N'789 Đường DEF, Đà Nẵng'),
('user000003', '123', N'Phạm Tâm', '0987654321', 'user456@example.com', N'456 Đường XYZ, Hà Nội'),
('user000004', '123', N'Phạm', '0912345678', 'user789@example.com', N'789 Đường DEF, Đà Nẵng'),
('user000005', '123', N'Nguyễn Thanh', '0987654321', 'user456@example.com', N'456 Đường XYZ, Hà Nội'),
('user000006', '123', N'Lê Văn C', '0912345678', 'user789@example.com', N'789 Đường DEF, Đà Nẵng');


delete from Users



select * from Trangthai 

INSERT INTO hoadon (maHoadon, diaChi, ngayTao, ngayGiao, id_Trangthai, tongTien, taikhoan)
VALUES 
('hd000001', N'Đà Nẵng', '2019-01-20', '2019-01-23', 1, 2000000, 'user000001');




INSERT INTO hoadon (maHoadon, diaChi, ngayTao, ngayGiao, id_Trangthai, tongTien, taikhoan)
VALUES 
('hd000002', N'Đà Nẵng', '2018-01-20', '2018-01-23', 1, 2000000, 'user000001'),
('hd000003', N'Đà Nẵng', '2024-01-20', '2024-01-23', 1, 2000000, 'user000001'),
('hd000004', N'Đà Nẵng', '2024-02-20', '2024-02-23', 1, 2000000, 'user000001'),
('hd000005', N'Đà Nẵng', '2024-11-20', '2024-11-23', 1, 2000000, 'user000001'),  
('hd000006', N'Đà Nẵng', '2018-11-20', '2018-11-23', 1, 2000000, 'user000001'),
('hd000007', N'Đà Nẵng', '2024-04-20', '2024-04-23', 1, 2000000, 'user000001'),
('hd000008', N'Đà Nẵng', '2024-05-20', '2024-05-23', 1, 2000000, 'user000001');





insert into Trangthai values 
			(0,N'Giỏ hàng'),
			(1,N'Đang sử lý'),
			(2,N'Đang giao'),
			(3,N'Hoàn thành'),
			(4,N'Thất bại')


insert into Trangthai values 
			(4,N'Chưa đánh giá'),
			(5,N'Đã đánh giá')