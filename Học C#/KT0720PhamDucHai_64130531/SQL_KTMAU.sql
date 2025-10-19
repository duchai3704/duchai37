CREATE DATABASE  KT0720_64130531;
GO
USE KT0720_64130531;
GO
CREATE TABLE LOP (
    MaLop CHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(50)
);
GO
CREATE TABLE SinhVien (
    MaSV varchar(10) PRIMARY KEY,
    HoSV NVARCHAR(50),
    TenSV NVARCHAR(50),
    NgaySinh DATE,
    GioiTinh BIT,
    AnhSV NVARCHAR(MAX),
    DiaChi NVARCHAR(100),
    MaLop CHAR(10),
    FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);
GO
-- Thêm dữ liệu vào bảng LỚP
INSERT INTO LOP (MaLop, TenLop) VALUES
('L01', N'Lớp 64CNTT1'),
('L02', N'Lớp 64CNTT2'),
('L03', N'Lớp 64CNTT3'),
('L04', N'Lớp 64CNTT4');
GO
-- Thêm dữ liệu vào bảng SINHVIEN với tên 3 chữ
INSERT INTO SINHVIEN (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, DiaChi, MaLop) VALUES
('64130001', N'Nguyễn Văn', N'Anh', '2002-05-12', 1, N'Hà Nội', 'L01'),
('64130002', N'Trần Thị', N'Hoa', '2003-03-09', 0, N'Hồ Chí Minh', 'L02'),
('64130003', N'Lê Minh', N'Tú', '2004-01-20', 1, N'Đà Nẵng', 'L03'),
('64130004', N'Phạm Ngọc', N'Huy', '2002-09-10', 1, N'Bình Dương', 'L01'),
('64130005', N'Ngô Thị', N'Lan', '2003-11-25', 0, N'Vũng Tàu', 'L02'),
('64130006', N'Hoàng Văn', N'Hải', '2004-02-14', 1, N'Quảng Ninh', 'L03'),
('64130007', N'Vũ Thanh', N'Trung', '2002-04-21', 1, N'Hải Phòng', 'L01'),
('64130008', N'Phan Thị', N'Thảo', '2003-06-07', 0, N'Nam Định', 'L02'),
('64130009', N'Đỗ Đức', N'Hoàng', '2004-08-15', 1, N'Ninh Bình', 'L03'),
('64130010', N'Bùi Thị', N'Thu', '2002-12-30', 0, N'Lào Cai', 'L01'),
('64130011', N'Nguyễn Thị', N'Phương', '2003-10-17', 0, N'Bắc Giang', 'L02'),
('64130012', N'Hoàng Anh', N'Quân', '2004-07-23', 1, N'Phú Yên', 'L03'),
('64130013', N'Lý Hoàng', N'Anh', '2002-03-03', 1, N'Khánh Hòa', 'L01'),
('64130014', N'Nguyễn Ngọc', N'Tuyết', '2003-01-11', 0, N'Bình Phước', 'L02'),
('64130015', N'Trần Minh', N'Hùng', '2004-09-25', 1, N'Bắc Ninh', 'L03'),
('64130016', N'Lê Thị', N'Hòa', '2002-11-14', 0, N'Hà Tĩnh', 'L01'),
('64130017', N'Phạm Quốc', N'Duy', '2003-05-04', 1, N'Bình Thuận', 'L02'),
('64130018', N'Ngô Thị', N'Vy', '2004-06-18', 0, N'Đồng Nai', 'L03'),
('64130019', N'Vũ Hoàng', N'Phúc', '2002-10-01', 1, N'Gia Lai', 'L01'),
('64130020', N'Trần Ngọc', N'Hiền', '2003-02-26', 0, N'Long An', 'L02');
GO
CREATE PROCEDURE SinhVien_TimKiem
    @MaSV varchar(8)=NULL,
    @HoTen nvarchar(50)=NULL,
    @MaLop CHAR(10) = NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
        @ParamList nvarchar(2000)
SELECT @SqlStr = '
    SELECT *
    FROM SinhVien
    WHERE (1=1) '
IF @MaSV IS NOT NULL
    SELECT @SqlStr = @SqlStr + '
        AND (MaSV = ''' + @MaSV + ''')'
IF @HoTen IS NOT NULL
    SELECT @SqlStr = @SqlStr + '
        AND (HoSV + N'' '' + TenSV LIKE N''%' + @HoTen + '%'')'
IF @MaLop IS NOT NULL
    SELECT @SqlStr = @SqlStr + 'AND (MaLop = ''' +  @MaLop + ''')'	

EXEC SP_EXECUTESQL @SqlStr

END


