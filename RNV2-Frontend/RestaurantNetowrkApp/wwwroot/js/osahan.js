//(function ($) {
//    "use strict";
//    $('[data-toggle="tooltip"]').tooltip();
//    $('.plus').on('click', function () { if ($(this).prev().val()) { $(this).prev().val(+$(this).prev().val() + 1); } });
//    $('.minus').on('click', function () { if ($(this).next().val() > 1) { if ($(this).next().val() > 1) $(this).next().val(+$(this).next().val() - 1); } });
//    $('.cat-slider').slick({ centerMode: true, slidesToShow: 4, centerPadding: '30px', slidesToScroll: 4, autoplay: true, autoplaySpeed: 2000, arrows: false });
//    $('.trending-slider').slick({ centerMode: true, centerPadding: '30px', slidesToShow: 1, arrows: false, responsive: [{ breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 2 } }, { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 1 } }] });
//    $('.osahan-slider').slick({ centerMode: false, slidesToShow: 1, arrows: false, dots: true });
//    $('.osahan-slider-map').slick({ centerMode: true, centerPadding: '30px', slidesToShow: 2, arrows: false, responsive: [{ breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 3 } }, { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 3 } }] });
//    var $main_nav = $('#main-nav');
//    var $toggle = $('.toggle');
//    var defaultOptions = { disableAt: false, customToggle: $toggle, levelSpacing: 40, navTitle: 'Askbootstrap', levelTitles: true, levelTitleAsBack: true, pushContent: '#container', insertClose: 2 };
//    var Nav = $main_nav.hcOffcanvasNav(defaultOptions);
//    var input = document.querySelector("#phone");
//})(jQuery);

window.homeSlick = () => {
    $('.cat-slider').slick({
        centerMode: true, slidesToShow: 4, centerPadding: '30px', slidesToScroll: 4, autoplay: true, autoplaySpeed: 2000, arrows: false,
        responsive: [{ breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 6 } },
        { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 4 } }]
    });
    $('#trending-slider').slick({
        centerMode: true, centerPadding: '30px', slidesToShow: 4, arrows: false,
        responsive: [{ breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 3 } },
        { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 2 } }]
    });
}
window.restaurantSlick = () => {
    $('.restaurant-slider').slick({
        centerMode: true, centerPadding: '30px', slidesToShow: 3, arrows: false,
        responsive: [{ breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: '40px', slidesToShow: 2 } }]
    });
}
window.restaurantQuantity = () => {
    $('.plus').on('click', function () { if ($(this).prev().val()) { $(this).prev().val(+$(this).prev().val() + 1); } });
    $('.minus').on('click', function () { if ($(this).next().val() > 1) { if ($(this).next().val() > 1) $(this).next().val(+$(this).next().val() - 1); } });
}

window.showItemInCart = (itemId,qty) => {
    var addStr = "add".concat(itemId);
    var quantityStr = "quantity".concat(itemId);
    var iptQtyStr = "iptQty".concat(itemId);
    var addDiv = document.getElementById(addStr);
    addDiv.style.display = "none";
    var quantityDiv = document.getElementById(quantityStr);
    quantityDiv.style.display = "flex";
    var iptQty = document.getElementById(iptQtyStr);
    iptQty.value = qty;

}