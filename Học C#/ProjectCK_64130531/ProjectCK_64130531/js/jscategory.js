let currentPage = 1;
const totalPages = 6; // Tổng số trang

function renderPagination() {
    const paginationNumbers = document.getElementById("paginationNumbers");
    paginationNumbers.innerHTML = "";

    // Tạo các nút trang
    for (let i = 1; i <= totalPages; i++) {
        const pageLink = document.createElement("a");
        pageLink.href = "#";
        pageLink.className = `page-number ${i === currentPage ? "active" : ""}`;
        pageLink.textContent = i;
        pageLink.setAttribute("data-page", i);
        pageLink.onclick = () => goToPage(i);
        paginationNumbers.appendChild(pageLink);
    }
}

function changePage(direction) {
    if (direction === "prev" && currentPage > 1) {
        currentPage--;
    } else if (direction === "next" && currentPage < totalPages) {
        currentPage++;
    }
    renderPagination();
    loadPageContent(currentPage);
}

function goToPage(page) {
    currentPage = page;
    renderPagination();
    loadPageContent(page);
}

function loadPageContent(page) {
    console.log(`Loading content for page ${page}...`);
    // Thay thế console.log bằng logic tải dữ liệu thực tế (AJAX/fetch)
}

// Khởi tạo phân trang
renderPagination();
