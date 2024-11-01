$(document).ready(function () {
    function initializePagination() {
        $(document).off('click', '.pagination a');
        $(document).on('click', '.pagination a', function (event) {
            event.preventDefault();
            var page = $(this).attr('href').split('pageNo=')[1].split('&')[0]; // Extract page number correctly
            var currentPath = window.location.pathname;
            var category = getParameterByName('category');

            if (currentPath.startsWith('/Admin')) {
                loadPage(page, '/Admin/Index', '#adminContent', category);
            } else {
                loadPage(page, '/Product/Index', '#catalog', category);
            }
        });
    }

    function loadPage(page, url, container, category) {
        $.ajax({
            url: url,
            type: 'GET',
            data: { pageNo: page, category: category },
            success: function (data) {
                $(container).html(data);
                initializePagination();
            },
            error: function (xhr, status, error) {
                console.error("Error loading page: ", status, error);
            }
        });
    }

    function getParameterByName(name) {
        var url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
        var results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }

    initializePagination();
});