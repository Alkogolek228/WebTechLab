$(document).ready(function () {
    $(document).on('click', '.pagination a', function (event) {
        event.preventDefault();
        var page = $(this).attr('href').split('pageNo=')[1];
        var currentPath = window.location.pathname;

        if (currentPath.startsWith('/Admin')) {
            loadPage(page, '/Admin/Index', '#adminContent');
        } else {
            loadPage(page, '/Product/Index', '#catalog');
        }
    });

    function loadPage(page, url, container) {
        $.ajax({
            url: url,
            type: 'GET',
            data: { pageNo: page },
            success: function (data) {
                $(container).html(data);
            },
            error: function (xhr, status, error) {
                console.error("Error loading page: ", status, error);
            }
        });
    }
});