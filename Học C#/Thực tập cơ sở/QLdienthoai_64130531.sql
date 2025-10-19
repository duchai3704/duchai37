CREATE DATABASE QLdienthoai_64130531
go
use QLdienthoai_64130531
go

CREATE TABLE PhanQuyen (
    IDQuyen INT IDENTITY(1,1) PRIMARY KEY,
    TenQuyen NVARCHAR(20)
);

CREATE TABLE Nguoidung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    Hoten NVARCHAR(50),
    Email NVARCHAR(50),
    Dienthoai CHAR(10),
    Matkhau VARCHAR(50),
    IDQuyen INT,
    Diachi NVARCHAR(100),
    Anhdaidien NCHAR(30),
    FOREIGN KEY (IDQuyen) REFERENCES PhanQuyen(IDQuyen)
);

CREATE TABLE Donhang (
    Madon INT IDENTITY(1,1) PRIMARY KEY,
    Ngaydat DATETIME,
    Tinhtrang INT,
    MaNguoidung INT,
    Thanhtoan INT,
    Diachinhanhang NVARCHAR(100),
    Tongtien DECIMAL(18, 0),
    FOREIGN KEY (MaNguoidung) REFERENCES Nguoidung(MaNguoiDung)
);

CREATE TABLE Hangsanxuat (
    Mahang INT IDENTITY(1,1) PRIMARY KEY,
    Tenhang NCHAR(10)
);

CREATE TABLE Hedieuhanh (
    Mahdh INT IDENTITY(1,1) PRIMARY KEY,
    Tenhdh NCHAR(10)
);

CREATE TABLE Sanpham (
    Masp INT IDENTITY(1,1) PRIMARY KEY,
    Tensp NVARCHAR(50),
    Giatien DECIMAL(18, 0),
    Soluong INT,
    Mota NTEXT,
    Thesim INT,
    Bonhotrong INT,
    Sanphammoi BIT,
    Ram INT,
    Anhbia NVARCHAR(50),
    Mahang INT,
    Mahdh INT,
    FOREIGN KEY (Mahang) REFERENCES Hangsanxuat(Mahang),
    FOREIGN KEY (Mahdh) REFERENCES Hedieuhanh(Mahdh)
);

CREATE TABLE Chitietdonhang (
    Madon INT,
    Masp INT,
    Soluong INT,
    Dongia DECIMAL(18, 0),-- và giatien cho đồng bộ
    Thanhtien DECIMAL(18, 0), 
    Phuongthucthanhtoan INT,-- để 1 pt tt  và nằm trong bảng đơn hàng
    PRIMARY KEY (Madon, Masp),
    FOREIGN KEY (Madon) REFERENCES Donhang(Madon),
    FOREIGN KEY (Masp) REFERENCES Sanpham(Masp)
);

-- 1. Insert PhanQuyen
SET IDENTITY_INSERT PhanQuyen ON;
INSERT INTO PhanQuyen (IDQuyen, TenQuyen)
VALUES 
(1, N'Member'),
(2, N'Admin');
SET IDENTITY_INSERT PhanQuyen OFF;


-- 2. Insert Nguoidung
SET IDENTITY_INSERT Nguoidung ON;
INSERT INTO Nguoidung (MaNguoiDung, Hoten, Email, Dienthoai, Matkhau, IDQuyen, Diachi, Anhdaidien)
VALUES 
(14, N'Man tran admin', N'Admin@gmail.com', N'0812883636', N'12345678', 2, N'Bình dương', N'/Images/files/ip3.jpg'),
(15, N'test', N'test@gmail.com', N'0812883636', N'12345678', 1, NULL, N'/Images/files/avt4.jpg'),
(16, N'Họ tên 11', N'Khach@gmail.com', N'0812883636', N'12345678', NULL, N'Bình dương', N'/Images/files/avt3.jpg'),
(36, N'mantran', N'mantran@gmail.com', N'0812883637', N'12345678', 1, NULL, NULL),
(39, N'Nguyễn Văn A', N'testa@gmail.com', N'0812883636', N'12345678', 1, NULL, NULL),
(41, N'Nguyễn Văn B', N'tetsb@gmail.com', N'0812883636', N'12345678', 1, NULL, NULL),
(42, N'Nguyễn Văn C', N'testc@gmail.com', N'0812883636', N'12345678', 1, NULL, NULL);
SET IDENTITY_INSERT Nguoidung OFF;

-- 3. Insert Hangsanxuat
SET IDENTITY_INSERT Hangsanxuat ON;
INSERT INTO Hangsanxuat (Mahang, Tenhang)
VALUES 
(1, N'Sam Sung'),
(2, N'Apple'),
(3, N'Xiaomi'),
(4, N'Vsmart'),
(5, N'Bphone');
SET IDENTITY_INSERT Hangsanxuat OFF;

-- 4. Insert Hedieuhanh
SET IDENTITY_INSERT Hedieuhanh ON;
INSERT INTO Hedieuhanh (Mahdh, Tenhdh)
VALUES 
(1, N'Android'),
(2, N'IOS'),
(3, N'VOS');
SET IDENTITY_INSERT Hedieuhanh OFF;

-- 5. Insert Sanpham
SET IDENTITY_INSERT Sanpham ON;
INSERT INTO Sanpham (Masp, Tensp, Giatien, Soluong, Mota, Thesim, Bonhotrong, Sanphammoi, Ram, Anhbia, Mahang, Mahdh)
VALUES 
(2, N'Apple Iphone 3', CAST(2000000 AS Decimal(18, 0)), 9, N'Apple Iphone 3', 1, 8, 0, 8, N'/Images/files/ip3.jpg', 2, 2),
(5, N'Apple Iphone 4', CAST(2000000 AS Decimal(18, 0)), 10, N'Apple Iphone 4', 1, 8, 0, 1, N'/Images/files/ip4.jpg', 2, 2),
(6, N'Apple Iphone 5', CAST(2000000 AS Decimal(18, 0)), 10, N'Apple Iphone 5', 1, 8, 0, 1, N'/Images/files/ip5.jpg', 2, 2),
(7, N'Apple Iphone 6', CAST(2000000 AS Decimal(18, 0)), 2, N'Apple Iphone 6', 1, 8, 0, 1, N'/Images/files/ip6.jpg', 2, 2),
(8, N'SamSung GalaxyS1', CAST(1000000 AS Decimal(18, 0)), 1, N'SamSung GalaxyS1', 2, 8, 0, 2, N'/Images/files/ss1.jpg', 1, 1),
(9, N'SamSung GalaxyS2', CAST(1000000 AS Decimal(18, 0)), 1, N'SamSung GalaxyS2', 1, 8, 0, 1, N'/Images/files/ss2.jpg', 1, 1),
(10, N'SamSung GalaxyS3', CAST(2000000 AS Decimal(18, 0)), 1, N'SamSung GalaxyS3', 1, 8, 0, 2, N'/Images/files/ss3.jpg', 1, 1),
(11, N'SamSung GalaxyS4', CAST(2000000 AS Decimal(18, 0)), 2, N'SamSung GalaxyS4', 2, 8, 0, 2, N'/Images/files/ss4.jpg', 1, 1),
(13, N'Xiaomi mi4', CAST(200000 AS Decimal(18, 0)), 10, N'Xiaomi mi4', 2, 8, 0, 4, N'/Images/files/mi4.jpg', 3, 1),
(14, N'Xiaomi mi5', CAST(2000000 AS Decimal(18, 0)), 10, N'Xiaomi mi5', 2, 16, 0, 6, N'/Images/files/mi5.jpg', 3, 1),
(15, N'Xiaomi mi6', CAST(20000 AS Decimal(18, 0)), 10, N'Xiaomi mi6', 2, 8, 0, 6, N'/Images/files/mi6.jpg', 3, 1),
(16, N'Xiaomi mi7', CAST(200000 AS Decimal(18, 0)), 10, N'Mi7', 2, 8, 0, 2, N'/Images/files/mi7.jpg', 3, 1);
SET IDENTITY_INSERT Sanpham OFF;
