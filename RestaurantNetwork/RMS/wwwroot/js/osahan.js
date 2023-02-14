(function ($) {
  "use strict";
  //   $("body").on("contextmenu", function (e) {
  //     return false;
  //   });

  // (function (i, s, o, g, r, a, m) {
  //   i["GoogleAnalyticsObject"] = r;
  //   (i[r] =
  //     i[r] ||
  //     function () {
  //       (i[r].q = i[r].q || []).push(arguments);
  //     }),
  //     (i[r].l = 1 * new Date());
  //   (a = s.createElement(o)), (m = s.getElementsByTagName(o)[0]);
  //   a.async = 1;
  //   a.src = g;
  //   m.parentNode.insertBefore(a, m);
  // })(window, document, "script", "../../../www.google-analytics.com/analytics.js", "ga");
  // ga("create", "UA-120909275-1", "auto");
  // ga("send", "pageview");
  $('[data-toggle="tooltip"]').tooltip();
  // $(".offer-slider").slick({
  //   slidesToShow: 4,
  //   arrows: true,
  //   responsive: [
  //     { breakpoint: 768, settings: { arrows: true, centerMode: true, centerPadding: "40px", slidesToShow: 2 } },
  //     { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: "40px", slidesToShow: 2 } },
  //   ],
  // });
  $(".cat-slider").slick({
    slidesToShow: 8,
    arrows: true,
    responsive: [
      { breakpoint: 768, settings: { arrows: true, centerMode: true, centerPadding: "40px", slidesToShow: 4 } },
      { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: "40px", slidesToShow: 4 } },
    ],
  });
  $(".trending-slider").slick({
    slidesToShow: 3,
    arrows: true,
    responsive: [
      { breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: "40px", slidesToShow: 2 } },
      { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: "40px", slidesToShow: 1 } },
    ],
  });
  $(".popular-slider").slick({
    centerMode: true,
    centerPadding: "30px",
    slidesToShow: 1,
    arrows: false,
    responsive: [
      { breakpoint: 768, settings: { arrows: false, centerMode: true, centerPadding: "40px", slidesToShow: 2 } },
      { breakpoint: 480, settings: { arrows: false, centerMode: true, centerPadding: "40px", slidesToShow: 1 } },
    ],
  });
  $(".osahan-slider").slick({ centerMode: false, slidesToShow: 1, arrows: false, dots: true });
  $(".osahan-slider-map").slick({
    autoplay: true,
    slidesToShow: 5,
    arrows: true,
    responsive: [
      { breakpoint: 768, settings: { arrows: false, autoplay: true, centerMode: true, centerPadding: "40px", slidesToShow: 3 } },
      { breakpoint: 480, settings: { arrows: false, autoplay: true, centerMode: true, centerPadding: "40px", slidesToShow: 3 } },
    ],
  });
  //var $main_nav = $("#main-nav");
 // var $toggle = $(".toggle");
 // var defaultOptions = { disableAt: false, customToggle: $toggle, levelSpacing: 40, navTitle: "Askbootstrap", levelTitles: true, levelTitleAsBack: true, pushContent: "#container", insertClose: 2 };
  //var Nav = $main_nav.hcOffcanvasNav(defaultOptions);
})(jQuery);
