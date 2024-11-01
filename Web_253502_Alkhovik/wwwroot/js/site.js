$(document).ready(function () {
    $(document).on('click', '.pagination a', function (event) {
        event.preventDefault();
        var page = $(this).attr('href').split('pageNo=')[1];
        loadPage(page);
    });

    function loadPage(page) {
        $.ajax({
            url: '/Product/Index',
            type: 'GET',
            data: { pageNo: page },
            success: function (data) {
                $('#catalog').html(data);
            }
        });
    }
});