
let debounceTimer;
// Hàm debounce để gọi API
function debouncedSearch() {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(function () {
        const query = document.getElementById("search_input").value;
        if (query) {
            console.log(query)
            searchAPI(query);  // Gọi API sau khi người dùng dừng gõ
        }
    }, 500); // Delay 500ms trước khi gọi API (có thể thay đổi giá trị này)
}

// Hàm gọi API và hiển thị kết quả
function searchAPI(query) {
    const encodedQuery = encodeURIComponent(query);
    console.log(encodedQuery)
    // Mã JavaScript cập nhật để gọi controller Search
    const encodedQuery = encodeURIComponent("Áo"); // Mã hóa từ khóa tìm kiếm

    fetch("/Home_64130531/Search?key=" + encodedQuery) // Thay URL API thành URL của action controller
        .then(response => {
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            return response.json();
        })
        .then(data => {
            // Kiểm tra dữ liệu trả về
            if (data.length > 0) { // Nếu danh sách sản phẩm không rỗng
                displayProducts(data); // Gọi hàm hiển thị sản phẩm
            } else {
                displayNoProductsFound(); // Gọi hàm hiển thị thông báo không tìm thấy sản phẩm
            }
        })
        .catch(error => {
            console.error("Error:", error);
        });
}
function displayProducts(products) {
    const productContainer = document.querySelector("main");
    productContainer.style.marginTop = "200px";
    productContainer.style.marginBottom = "100px";// Thêm margin-top
    productContainer.style.marginLeft = "10%";     // Thêm margin-left (căn giữa)
    productContainer.style.marginRight = "10%";    // Thêm margin-right (căn giữa)
    productContainer.style.width = "80%";          // Đặt width cho phần tử
    productContainer.style.minHeight = "500px";
    productContainer.style.display = "flex";

    // Xóa tất cả sản phẩm cũ
    productContainer.innerHTML = "";

    // Lặp qua các sản phẩm và tạo HTML cho từng sản phẩm
    products.forEach(product => {
        // Tạo một phần tử cho từng sản phẩm
        const productElement = document.createElement("div");
        productElement.classList.add("col-lg-3", "col-md-6");  // Đảm bảo các lớp phù hợp với bố cục của bạn

        // Thêm HTML của sản phẩm
        productElement.innerHTML = `
    <div class="single-product" >
        <img class="img-fluid" src="/img/product/${product.hinhanh}" alt="${product.tensanpham}" onclick="window.location.href='/Home/ChitietSanpham?masanpham=${product.masanpham}&macolor=${product.mamau}&masize=${product.masize}'">
            <div class="product-details">
                <h6>${product.tensanpham}</h6>
                <div class="price">
                    <h6>$${product.gia}</h6>
                    <h6 class="l-through">$210.00</h6>  <!-- Nếu bạn có giá cũ, có thể thay $210.00 bằng giá cũ -->
                </div>
                <div class="prd-bottom">
                  <a href="#" class="social-info" onclick="addToCart('${product.masanpham.slice(-6)}'+'0140')">Add to Cart</a>
                        <span class="ti-bag"></span>
                        <p class="hover-text">Add to bag</p>
                    </a>
                    <a href="#" class="social-info">
                        <span class="lnr lnr-move"></span>
                        <p class="hover-text" onclick="window.location.href='/Home/ChitietSanpham?masanpham=${product.masanpham}&macolor=${product.mamau}&masize=${product.masize}'">View more</p>
                    </a>
                </div>
            </div>
    </div>
    `;

        // Thêm sản phẩm vào container có lớp row
        productContainer.appendChild(productElement);
    });
}

function displayNoProductsFound() {
    const productContainer = document.getElementById("product-list");

    // Xóa tất cả sản phẩm cũ
    productContainer.innerHTML = "";

    // Tạo phần tử <h3> hiển thị thông báo "Không có sản phẩm"
    const noProductsElement = document.createElement("h3");
    noProductsElement.textContent = "Không có sản phẩm";
    noProductsElement.style.textAlign = "center";  // Căn giữa thông báo
    noProductsElement.style.marginTop = "20px";  // Tạo khoảng cách

    // Thêm thông báo vào container
    productContainer.appendChild(noProductsElement);
}

