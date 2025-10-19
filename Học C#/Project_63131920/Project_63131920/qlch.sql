create database QLCH_63131920
GO
USE QLCH_63131920
CREATE TABLE "Variation"(
    "id" INT IDENTITY(1,1) primary key,
    "category_id" INT NOT NULL,
    "name" NVARCHAR(255) NOT NULL
);


CREATE TABLE "Shopping_cart_item"(
    "id" INT IDENTITY(1,1) primary key,
    "cart_id" INT NOT NULL,
    "qty" INT NOT NULL,
    "product_id" INT NOT NULL
);

CREATE TABLE "Product_item"(
    "id" INT IDENTITY(1,1) primary key,
    "product_id" INT NOT NULL,
    "qty_in_stock" INT NOT NULL,
    "product_image" NVARCHAR(255),
    "price" INT NOT NULL
);

CREATE TABLE "Variation_option"(
    "id" INT IDENTITY(1,1) primary key,
    "variation_id" INT NOT NULL,
    "value" VARCHAR(255) NOT NULL
);

CREATE TABLE "Order_details"(
    "id" INT IDENTITY(1,1) primary key,
    "order_id" INT NOT NULL,
    "product_item_id" INT NOT NULL,
    "price" INT NOT NULL,
    "quantity" INT NOT NULL
);

CREATE TABLE "Shop_order"(
    "id" INT IDENTITY(1,1) primary key,
    "user_id" INT NOT NULL,
    "order_date" DATETIME NOT NULL,
    "shipping_address" INT NOT NULL,
    "shipping_method" INT NOT NULL,
    "order_total" INT NOT NULL,
    "order_status" BIT NOT NULL,
    "payment_method" NVARCHAR NOT NULL
);

CREATE TABLE "User"(
    "id" INT IDENTITY(1,1) primary key,
    "fullname" NVARCHAR(255) NOT NULL,
    "email" VARCHAR(255) NOT NULL,
    "phone_number" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
	"sex" BIT NOT NULL
);


CREATE TABLE "Shopping_cart"(
    "id" INT IDENTITY(1,1) primary key,
    "user_id" INT NOT NULL
);

CREATE TABLE "Shipping_method"(
    "id" INT IDENTITY(1,1) primary key,
    "name" NVARCHAR(255) NOT NULL,
    "price" INT NOT NULL
);

CREATE TABLE "Product_config"(
    "product_item_id" INT NOT NULL,
    "variation_option_id" INT NOT NULL
	PRIMARY KEY (product_item_id, variation_option_id)
);
CREATE TABLE "Category"(
    "id" INT IDENTITY(1,1) primary key,
    "name" NVARCHAR(255) NOT NULL
);

CREATE TABLE "User_address" (
	"user_id" int not null,
	"address_id" int not null,
	PRIMARY KEY ("user_id","address_id" )
);

CREATE TABLE "Admin"(
   "id" INT IDENTITY(1,1) primary key,
    "fullname" NVARCHAR(255) NOT NULL,
   "email" VARCHAR(100) NOT NULL,
   "password" VARCHAR(50) NOT NULL
);

INSERT INTO Admin(fullname, email, "password")
VALUES (N'admin', 'dat@gmail.com', '123')



CREATE TABLE "Address" (
    "id" INT IDENTITY(1,1) primary key,
    "province" NVARCHAR(255) NOT NULL,
    "city" NVARCHAR(255) NOT NULL,
    "street_number" NVARCHAR(255) NOT NULL,
    "district" NVARCHAR(255) NOT NULL
);


CREATE TABLE "Products"(
    "id" INT IDENTITY(1,1) primary key,
    "product_name" NVARCHAR(255) NOT NULL,
    "category_id" INT NOT NULL,
    "price" INT NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "qty_in_stock" INT NOT NULL,
    "product_image" NVARCHAR(255),
);



/*CREATE TABLE "Promotion"(
    "id" INT IDENTITY(1,1) primary key,
    "name" NVARCHAR(255) NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "discount_rate" FLOAT NOT NULL,
    "start_date" DATE NOT NULL,
    "end_date" DATE NOT NULL
);*/

ALTER TABLE
    "Order_details" ADD CONSTRAINT "order_details_order_id_foreign" FOREIGN KEY("order_id") REFERENCES "Shop_order"("id");
ALTER TABLE
    "Shopping_cart_item" ADD CONSTRAINT "shopping_cart_item_product_id_foreign" FOREIGN KEY("product_id") REFERENCES "Product_item"("id");
ALTER TABLE
    "Product_config" ADD CONSTRAINT "product_config_variation_option_id_foreign" FOREIGN KEY("variation_option_id") REFERENCES "Variation_option"("id");
ALTER TABLE
    "Shop_order" ADD CONSTRAINT "shop_order_user_id_foreign" FOREIGN KEY("user_id") REFERENCES "User"("id");
/*ALTER TABLE
    "Products" ADD CONSTRAINT "products_promotion_id_foreign" FOREIGN KEY("promotion_id") REFERENCES "Promotion"("id");*/
ALTER TABLE
    "Order_details" ADD CONSTRAINT "order_details_product_item_id_foreign" FOREIGN KEY("product_item_id") REFERENCES "Product_item"("id");
ALTER TABLE
    "Product_config" ADD CONSTRAINT "product_config_product_item_id_foreign" FOREIGN KEY("product_item_id") REFERENCES "Product_item"("id");
--ALTER TABLE
--    "User" ADD CONSTRAINT "user_address_id_foreign" FOREIGN KEY("address_id") REFERENCES "Address"("id");
ALTER TABLE
    "Shopping_cart" ADD CONSTRAINT "shopping_cart_customer_id_foreign" FOREIGN KEY("user_id") REFERENCES "User"("id");
ALTER TABLE
    "Variation_option" ADD CONSTRAINT "variation_option_variation_id_foreign" FOREIGN KEY("variation_id") REFERENCES "Variation"("id");
ALTER TABLE
    "Shopping_cart_item" ADD CONSTRAINT "shopping_cart_item_cart_id_foreign" FOREIGN KEY("cart_id") REFERENCES "Shopping_cart"("id");
ALTER TABLE
    "Variation" ADD CONSTRAINT "variation_category_id_foreign" FOREIGN KEY("category_id") REFERENCES "Category"("id");
ALTER TABLE
    "Products" ADD CONSTRAINT "products_category_id_foreign" FOREIGN KEY("category_id") REFERENCES "Category"("id");
ALTER TABLE
    "Shop_order" ADD CONSTRAINT "shop_order_shipping_method_foreign" FOREIGN KEY("shipping_method") REFERENCES "Shipping_method"("id");
ALTER TABLE
    "Product_item" ADD CONSTRAINT "product_item_id_foreign" FOREIGN KEY("product_id") REFERENCES "Products"("id");
ALTER TABLE
    "Shop_order" ADD CONSTRAINT "shop_order_shipping_address_foreign" FOREIGN KEY("shipping_address") REFERENCES "Address"("id");
ALTER TABLE
	"User_address" ADD CONSTRAINT "user_address_user" FOREIGN KEY ("user_id") REFERENCES "User"("id");
ALTER TABLE
	"User_address" ADD CONSTRAINT "user_address_address" FOREIGN KEY ("address_id") REFERENCES "Address"("id");

INSERT INTO Category (name)
VALUES 
(N'Polo'),
(N'Shirt'),
(N'Blazer tay lỡ'),
(N'Quần Jean'),
(N'Áo khoác'),
(N'Sweater'),
(N'Quần âu'),
(N'Quần Short');



INSERT INTO Products (product_name, category_id, price, description, qty_in_stock, product_image)
VALUES 
-- blazer
(N'Blazer Supreme Blue', 3, 150000, N'Áo Blazer Vest 1 Lớp Nam MANDO Tay Lỡ Vải Tuysi Họa Tiết Caro Xám 2 Hàng Khuyu Basic Unisex Thời Trang Hàn Quốc AVH009', 10, 'blazer1.webp'),
(N'Áo Blazer Classic Black Elegance', 3, 250000, N' Áo Blazer Nam Tay Lỡ MANDO Form Rộng Kiểu Dáng Thời Trang Hàn Quốc Basic Gắn Dây Xích Phong Cách AVH010', 20, 'blazer2.webp'),
(N'Summer Breeze Linen Blazer', 3, 350000, N' Áo Blazer Nam Tay Lỡ MANDO 2 Màu Xanh Đen Form Rộng Kiểu Dáng Thời Trang Hàn Quốc Basic AVH011', 30, 'blazer3.webp'),
--bomber
(N'Skyline Explorer Bomber', 5, 349000, N' Áo Khoác Bomber Nam MANDO Vải Gió 2 Lớp Chống Thấm Cản Gió, Bo Gấu Kiểu Dáng Năng động Phong Cách Hàn Quốc BOB002', 10, 'akhoac1.webp'),
(N'Midnight Stealth Bomber Jacket', 5, 199000, N' Áo Khoác Bomber Nam MANDO Vải Gió 2 Lớp Chống Thấm Cản Gió, Bo Gấu Kiểu Dáng Năng động Phong Cách Hàn Quốc BOB002', 20, 'akhoac2.webp'),

(N'Áo Blazer Vintage Brown Tweed', 3, 450000, N' Áo Blazer Nam Tay Lỡ MANDO Form Rộng Kiểu Dáng Thời Trang Hàn Quốc Basic Họa Tiết Thêu Would You AVH002', 40, 'blazer4.webp'),
(N'Urban Explorer Grey Blazer', 3, 650000, N' Áo Blazer Nam Tay Lỡ MANDO Form Rộng Kiểu Dáng Thời Trang Hàn Quốc Basic Gắn Dây Xích Phong Cách AVH010', 50, 'blazer5.webp'),
--short
(N' Quần Short Nam Thể Thao', 8, 259999, N' Quần Short Nam Thể Thao MANDO Phong Cách Hàn Quốc, Chất Liệu Thun Tổ Ong Co Giãn Tốt Chống Nhăn, Thấm Hút Mồ Hôi QSN037', 50, 'qshort2.webp'),
(N'QUẦN SHORTS CẠP CHUN', 8, 359999, N' QUẦN SHORTS CẠP CHUN PHỐI SƯỜN QSN038', 50, 'qshort3.webp'),

(N'Áo Blazer Velvet', 3, 150000, N' Áo Blazer Tay Lỡ Nam Cổ Ve Xuôi MANDO Chất Liệu Flannel Cao Cấp, Mềm Mịn Không Co Nhăn, Thiết Kế Basic AVH017', 50, 'blazer6.webp'),
(N'Casual Khaki Linen Blazer', 3, 150000, N' BLAZER TAY LỠ AVH014', 30, 'blazer2.webp'),
(N'Essential Beige Summer Blazer', 3, 150000, N' Áo Blazer Dài Tay MANDO Vải Tuyết Mưa Nam Nữ Form Rộng Họa Tiết Thêu Phong Cách Hàn Quốc Basic BLZ001', 30, 'blazer7.webp'),
(N'Sporty Grey Knit Blazer', 3, 150000, N' Áo Blazer Nam 1 Lớp  MANDO Dài Tay 3 Màu Vải Tuyết Mưa Cao Cấp Trơn Không Họa Tiết Basic Thời Trang Hàn Quốc BLZ006', 30, 'blazer8.webp'),
--bommber
(N'Retro Vibe Camo Bomber', 5, 299999, N' Áo Khoác Bomber Varsity Nam MANDO Vải Gió Phối Tay Form Rộng Phong Cách Unisex Thời Trang Hàn Quốc BOB010', 100, 'akhoac3.webp'),
(N'Urban Nomad Utility Bomber', 5, 159999, N' Áo Khoác Bomber Varsity Nhung Tăm Nam MANDO 3 Màu Form Rộng Basic Thể Thao Unisex Thời Trang Hàn Quốc BOB001 - BOB006', 50, 'akhoac4.webp'),
--polo
(N'Áo Polo Classic Comfort', 1, 349000, N' Áo Phông Polo Nam Màu Trắng MANDO Tay Lỡ Cotton Cao Cấp Form Rộng Basic Unisex Thời Trang Hàn Quốc PDN122 POL001', 10, 'polo1.webp'),
(N'Áo Polo Dynamic Edge', 1, 199000, N' Áo Phông Polo Trắng Nam Sọc Tay Đen MANDO Tay Lỡ Cotton Cao Cấp Form Rộng Basic Unisex Thời Trang Hàn Quốc PDN130', 20, 'polo2.jpg'),
(N'Áo Polo Coastal Breeze', 1, 299999, N' POLO SIGNATURE PDN143', 100, 'polo3.webp'),
(N'Áo Polo Elite Performance', 1, 159999, N' Áo Polo Nam Cổ Chữ V Không Cúc MANDO Chất Liệu Cá Sấu TC Có Hình Thêu Ngực Phong Cách Hàn Quốc POL009', 50, 'polo4.jpg'),
(N'Áo Polo Weekend Escape', 1, 159999, N' Áo Polo Nam MANDO Tay Lỡ Unisex Vải Cá Sấu TC Thoáng Mát Phong Cách Trẻ Trung Hàn Quốc POL002', 50, 'polo5.webp'),
-- shirt
(N'Sơ Mi Oxford Essential', 2, 159999, N' Áo Sơ Mi Đen Nam Cuban Shirt Ngắn Tay MANDO Họa Tiết Vải Lụa Form Rộng Unisex Thời Trang Hàn Quốc SMD122', 40, 'shirt2.jpg'),
(N'Sơ Mi Denim Classic', 2, 159999, N' Áo Sơ Mi Caro Nam MANDO Form Rộng Dài Tay Kiểu Dáng Basic Unisex Thời Trang Hàn Quốc SMD111 - SMD112', 50, 'shirt1.webp'),
-- quần jean
(N'Quần Jean Nam MANDO Ống Rộng', 4, 159999, N' Quần Jean Nam MANDO Ống Rộng Chất Bò Xanh Nhạt Kiểu Dáng Basic Unisex Thời Trang Hàn Quốc  QJN057', 40, 'qjean1.webp'),
(N'Quần Jean Nam MANDO Rách Gối Ống Suông', 4, 259999, N' Quần Jean Nam MANDO Rách Gối Ống Suông Form Rộng Kiểu Dáng Basic Unisex Thời Trang Hàn Quốc QJ38', 50, 'qjean2.webp'),
(N'Quần Jean Nam Rách Gối MANDO Vải Bò', 4, 359999, N' Quần Jean Nam Rách Gối MANDO Vải Bò Cao Cấp 2 Màu Xanh Đen Form Suông Rộng Basic Unisex Thời Trang Hàn Quốc QJN056', 50, 'sqjean3.jpg'),
-- quần âu
(N'Quần Âu Dáng Suông MANDO', 7, 159999, N' Quần Âu Dáng Suông MANDO Nam Chất Liệu Tuyết Mưa Dày Dặn, Co Giãn Phối Khuy Phong Cách Unisex TRS006', 40, 'qau1.webp'),
(N'QUẦN ÂU HÀN NAM DÁNG XUÔNG', 7, 259999, N' QUẦN ÂU HÀN NAM DÁNG XUÔNG KÈM ĐAI MANDO - TRS004', 50, 'qau2.webp'),
(N'Quần Âu Nam Cạp Chun Sau', 7, 359999, N' Quần Âu Nam Cạp Chun Sau MANDO Có Túi Form Ống Rộng Phong Cách Basic Thể Thao Thời Trang Hàn Quốc TRS011', 50, 'qau3.webp'),
-- quần short
(N'Quần Short Nam MANDO', 8, 159999, N' Quần Short Nam MANDO Chất Vải Tuyết Mưa Thoáng Mát Phối Phụ Kiện Kim Loại Kiểu Dáng Hàn Quốc SHR001', 40, 'qshort1.webp'),

--sweater
(N'Áo Giữ Nhiệt Cổ Cao 3 Phân MANDO', 6, 159999, N' Áo Giữ Nhiệt Cổ Cao 3 Phân MANDO Chất Nỉ Unisex Form Rộng Trơn Basic Phong Cách Hàn Quốc SWT005', 40, 'sweater1.webp'),
(N'Áo Len Nam Hàn Quốc Cổ Tròn', 6, 259999, N' Áo Len Nam Hàn Quốc Cổ Tròn MANDO Áo Dài Tay Form Rộng, Có 3 Màu, Chất Liệu Gân Cao Cấp, Mềm Mại, Giữ Ấm Tốt SWT008', 50, 'sweater2.webp'),
(N'Áo Nỉ Cao Cấp Sweater Dài Tay', 6, 359999, N' Áo Nỉ Cao Cấp Sweater Dài Tay Nam MANDO 2 Màu Họa Tiết In Chữ Form Rộng Basic Unisex Thời Trang Hàn Quốc SWT006', 50, 'sweater3.webp')
