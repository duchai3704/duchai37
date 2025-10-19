function updateItemsPerPage() {
    var itemsPerPage = document.getElementById("itemsPerPage").value;
    var currentPage = @ViewBag.CurrentPage;  // Giá trị từ ViewBag được chèn vào trực tiếp khi trang được render
    var sortOption = document.getElementById("sortOptions").value;

    // Cập nhật lại URL dựa trên các giá trị hiện tại
    window.location.href = '@Url.Action("Category_64130531", new { page = "__page__", pageSize = "__pageSize__", sort = "__sort__" })'
        .replace("__page__", currentPage)
        .replace("__pageSize__", itemsPerPage)
        .replace("__sort__", sortOption);
}

function updateSort() {
    var sortOption = document.getElementById("sortOptions").value;
    var currentPage = @ViewBag.CurrentPage;  // Giá trị từ ViewBag được chèn vào trực tiếp khi trang được render
    var itemsPerPage = document.getElementById("itemsPerPage").value;

    // Cập nhật lại URL dựa trên các giá trị hiện tại
    window.location.href = '@Url.Action("Category_64130531", new { page = "__page__", pageSize = "__pageSize__", sort = "__sort__" })'
        .replace("__page__", currentPage)
        .replace("__pageSize__", itemsPerPage)
        .replace("__sort__", sortOption);
}
