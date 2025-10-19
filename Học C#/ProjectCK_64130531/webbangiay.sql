create database QLBanGiay_64130531
go
use QLBanGiay_64130531
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
-- Table: Danhmuc
CREATE TABLE Danhmuc (
    maDanhmuc INT PRIMARY KEY,
    tenDanhmuc NVARCHAR(50)
);

-- Table: Nhanhieu
CREATE TABLE Nhanhieu (
    maNhan INT PRIMARY KEY,
    tenNhan NVARCHAR(50),
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

-- Table: Size
CREATE TABLE Size (
    maSize INT PRIMARY KEY,
    chieudai FLOAT,
    chieurong FLOAT,
    chieucao FLOAT
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
    noidung TEXT,
    ngaytao DATE default GETDATE(),
    hinhanh NVARCHAR(100),
	maDanhmuc INT not null,
	FOREIGN KEY ( maDanhmuc) REFERENCES Danhmuc( maDanhmuc)
);


-- Table: Sanpham
CREATE TABLE Sanpham (
    maSanpham CHAR(10) PRIMARY KEY,
    tenSanpham NVARCHAR(100),
    moTa TEXT,
    soLuongyeuthich INT not null default 0,
    maNhan INT not null,
    maDanhmuc INT not null,
	maKhuyenmai CHAR(10) not null,
    FOREIGN KEY (maNhan) REFERENCES Nhanhieu(maNhan),
    FOREIGN KEY (maDanhmuc) REFERENCES Danhmuc(maDanhmuc),
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
    maSize INT not null ,
    FOREIGN KEY (maSanpham) REFERENCES Sanpham(maSanpham),
    FOREIGN KEY (maMau) REFERENCES Color(maMau),
    FOREIGN KEY (maSize) REFERENCES Size(maSize)
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

select * from sanpham,chitietsanpham,color,size




insert into danhmuc values(1,N'Thể thao'),
	(2,N'Casual'),
    (3,N'Giày Công Sở'),
	(4,N'Giày Cao Gót'),
	(5,N'Giày Lười'),
	(6,N'Thể thao')

select * from danhmuc
delete from danhmuc

insert into color values(1,N'Xanh'),
							(2,N'Trắng'),
							(3,N'Vàng'),
							(4,N'Tím')


INSERT INTO Size (maSize, chieudai, chieurong, chieucao) 
VALUES 
(40, 25.0, 9.0, 5.0),  -- Size 40
(41, 25.5, 9.5, 5.0),  -- Size 41
(42, 26.0, 10.0, 5.5), -- Size 42
(43, 26.5, 10.5, 6.0), -- Size 43
(44, 27.0, 11.0, 6.0); 


insert into Nhanhieu values (1,N'Nike'),
(2,N'Adidas'),
(3,N'Converse'),
(4,N'Fila')

select * from Nhanhieu
delete from Nhanhieu

INSERT INTO Sanpham (maSanpham, tenSanpham, moTa, maNhan, maDanhmuc, maKhuyenmai) 
VALUES 
('SP000001', N'Nike ZoomX Vaporfly', N'Giày dáng thấp với phối màu đa dạng, ban đầu được thiết kế cho bóng rổ nhưng đã trở thành biểu tượng thời trang.', 1, 1, 'KM0001'),
('SP000002', N'Adidas Ultraboost', N'Một mẫu giày biểu tượng với thiết kế cổ điển, phần đế cao và lớp đệm Air, phù hợp cho cả thể thao và thời trang đường phố.', 2, 2, 'KM0002'),
('SP000003', N'Converse Jack Purcell', N'Công nghệ Fresh Foam mang lại cảm giác nhẹ nhàng và hỗ trợ cho các buổi chạy dài.', 3, 1, 'KM0002'),
('SP000004', N'Converse Chuck 70', N'Một đôi giày đa năng với thiết kế tối giản và đệm êm ái, phù hợp cho mọi dịp.', 3, 2, 'KM0002'),
('SP000005', N'Fila Axilus 2 Energized', N'Mẫu giày dành cho tennis với thiết kế nhẹ, đệm êm và độ bám tốt.', 4, 2, 'KM0002'),
('SP000006', N'Adidas Yeezy Boost 350', N'Nổi tiếng với công nghệ đệm Boost, mang lại sự êm ái tối ưu, phù hợp cho chạy bộ và sử dụng hàng ngày.', 2, 3, 'KM0002'),
('SP000007', N'Fila Ray Tracer', N'Mẫu giày chạy bộ với công nghệ Gel giúp giảm chấn, rất phù hợp cho chạy đường dài.', 4, 3, 'KM0002'),
('SP000008', N'Nike Air Max', N'Đặc trưng bởi bộ đệm Air lộ ra ngoài ở phần gót, mang lại cảm giác êm ái và phong cách hiện đại.', 1, 4, 'KM0002'),
('SP000009', N'Converse Chuck Taylor All Star', N'Mang phong cách retro với phần lưỡi gà tách đôi độc đáo, phù hợp cho thời trang thường ngày.', 3, 4, 'KM0002');


ALTER TABLE Sanpham
ALTER COLUMN moTa NVARCHAR(MAX);

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

INSERT INTO Chitietsanpham (id_chitietSP, hinhAnh, soLuongTon, gia, maMau, maSanpham, maSize, tenMau)
VALUES 
('0000010140', 'md-2-xanh.png', 0, 599.99, 2, 'SP000001', 40, N'Xanh'), 
('0000010141', 'giay-trang-2.jpg', 200, 699.99, 1, 'SP000001', 41, N'Trắng'), 
('0000010242', 'giay-trang.png', 150, 799.99, 2, 'SP000003', 42, N'Trắng'),
('0000020140', 'giay-vang-6.jpg', 100, 599.99, 3, 'SP000001', 40, N'Vàng'), 
('0000020141', 'giay-xanh-5.png', 200, 699.99, 1, 'SP000002', 41, N'Xanh'),
('0000020242', 'giay-trang-6.png', 150, 799.99, 2, 'SP000001', 42, N'Trắng'),

('0000030140', 'giay-xanh-2.png', 100, 599.99, 1, 'SP000004', 40, N'Xanh'), 
('0000030141', 'giay-vang.png', 200, 699.99, 3, 'SP000002', 41, N'Vàng'),
('0000040140', 'giay-xanh-4.png', 50, 599.99, 1, 'SP000002', 40, N'Xanh'), 
('0000040141', 'giay-xanh-5.png', 75, 699.99, 1, 'SP000005', 41, N'Xanh'), 
('0000040242', 'giay-trang-7.png', 120, 799.99, 2, 'SP000002', 42, N'Trắng'),
('0000050140', 'giay-xanh-3.jpg', 80, 499.99, 1, 'SP000002', 40, N'Xanh'),

('0000050141', 'giay-vang-1.png', 150, 599.99, 3, 'SP000003', 41, N'Vàng'),
('0000050242', 'giay-trang-4.png', 90, 699.99, 2, 'SP000004', 42, N'Trắng'),
('0000060140', 'giay-tim-1.jpg', 70, 599.99, 4, 'SP000003', 40, N'Tím'), 
('0000060141', 'giay-tim-2.png', 130, 699.99, 4, 'SP000005', 41, N'Tím'), 
('0000060242', 'giay-xanh-6.png', 100, 799.99, 1, 'SP000003', 42, N'Xanh'),
('0000070140', 'giay-trang-3.jpg', 60, 499.99, 2, 'SP000007', 40, N'Trắng'),

('0000070141', 'giay-trang-2.jpg', 90, 599.99, 2, 'SP000009', 41, N'Trắng'), 
('0000070242', 'giay-trang-1.jpg', 110, 699.99, 2, 'SP000008', 42, N'Trắng'),
('0000080140', 'giay-vang-4.jpg', 85, 599.99, 3, 'SP000007', 40, N'Vàng'), 
('0000080141', 'md1-xanh.png', 95, 699.99, 1, 'SP000006', 41, N'Xanh'),
('0000080242', 'giay-trang-5.png', 70, 799.99, 2, 'SP000009', 42, N'Trắng'),
('0000090140', 'giay-tim-3.jpg', 50, 499.99, 4, 'SP000006', 40, N'Tím');

ALTER TABLE Chitietsanpham
ADD tenMau NVARCHAR(50);


DELETE FROM Chitietsanpham
select * from Chitietsanpham

	select * from Users

	INSERT INTO Users (taikhoan, matkhau, ten, sdt, email, diaChi)
VALUES 
('user000001', 'password456', N'Trần Thị B', '0987654321', 'user456@example.com', N'456 Đường XYZ, Hà Nội'),
('user000002', '123', N'Lê Văn C', '0912345678', 'user789@example.com', N'789 Đường DEF, Đà Nẵng'),
('user000003', '123', N'Phạm Tâm', '0987654321', 'user456@example.com', N'456 Đường XYZ, Hà Nội'),
('user000004', '123', N'Phạm', '0912345678', 'user789@example.com', N'789 Đường DEF, Đà Nẵng'),
('user000005', '123', N'Nguyễn Thanh', '0987654321', 'user456@example.com', N'456 Đường XYZ, Hà Nội'),
('user000006', '123', N'Lê Văn C', '0912345678', 'user789@example.com', N'789 Đường DEF, Đà Nẵng');


select * from Chitiethoadon



select * from hoadon 

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
			(1,N'Đang xử lý'),
			(2,N'Đang giao'),
			(3,N'Hoàn thành'),
			(4,N'Thất bại')

			select * from trangthai
			
			
			


			




