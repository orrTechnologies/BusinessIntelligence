$(window).load(function () {

    $("#page-search-button").click(function () {
       var searchTerm = $('#page-search-input-box').val();
        if (searchTerm) {
            window.location = ('/facebook/search/?query=' + searchTerm);
        }
    });

        Morris.Donut(donutChartParams);
        Morris.Area(areaChartParams);
});