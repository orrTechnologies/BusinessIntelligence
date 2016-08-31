$(window).load(function () {

    $("#page-search-button").click(function () {
       var searchTerm = $('#page-search-input-box').val();
        if (searchTerm) {
            window.location = ('/facebook/search/?query=' + searchTerm);
        }
    });
    $('#page-search-input-box input').keydown(function (e) {
        if (e.keyCode == 13) {
            $('#form').submit();
        }
    });

        Morris.Donut(donutChartParams);
        Morris.Area(areaChartParams);
});