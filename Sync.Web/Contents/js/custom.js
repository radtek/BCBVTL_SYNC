$(document).ready(function () {

    var heightw = $(window).height();
    var a = heightw - 45;
    $(".bg-content").height(a);
    var Hmap = a - 175;
    $(".bg-map").height(Hmap);
    // $(".vitri").height(Hmap);
    var Himg = $(".img-bg").height();
    var wMap = Himg * 2;
    $(".vitri").width(wMap);

    // image1
    $(".vitri img:first-child").css({
        "top": Himg / 6.7,
        "right": wMap / 1.037
    });
    //end  image1

    // image2
    $(".vitri img:nth-child(2)").css({
        "top": Himg / 2.95,
        "left": wMap / 9.6
    });
    //end  image2

    // image3
    $(".vitri img:nth-child(3)").css({
        "top": Himg / 130,
        "left": wMap / 5.8
    });
    //end  image3

    // image4
    $(".vitri img:nth-child(4)").css({
        "top": Himg / 1.37,
        "left": wMap / 3.92
    });
    //end  image4

    // image5
    $(".vitri img:nth-child(5)").css({
        "top": Himg / 1.09,
        "left": wMap / 3.93
    });
    //end  image5

    // image6
    $(".vitri img:nth-child(6)").css({
        "top": Himg / 7.6,
        "left": wMap / 3.24
    });
    //end  image6

    // image7
    $(".vitri img:nth-child(7)").css({
        "top": Himg / 3.7,
        "left": wMap / 3.63
    });
    //end  image7

    // image8
    $(".vitri img:nth-child(8)").css({
        "top": Himg / 1.8,
        "left": wMap / 3.07
    });
    //end  image8

    // image9
    $(".vitri img:nth-child(9)").css({
        "top": Himg / 100,
        "left": wMap / 2.575
    });
    //end  image9

    // image10
    $(".vitri img:nth-child(10)").css({
        "top": Himg / 12,
        "left": wMap / 2.07
    });
    //end  image10

    // image11
    $(".vitri img:nth-child(11)").css({
        "top": Himg / 3,
        "left": wMap / 1.8
    });
    //end  image11

    // image12
    $(".vitri img:nth-child(12)").css({
        "top": Himg / 1.58,
        "left": wMap / 1.85
    });
    //end  image12

    // image13
    $(".vitri img:nth-child(13)").css({
        "top": Himg / 3.7,
        "left": wMap / 1.48
    });
    //end  image13

    // image14
    $(".vitri img:nth-child(14)").css({
        "top": Himg / 2.12,
        "left": wMap / 1.42
    });
    //end  image14

    // image15
    $(".vitri img:nth-child(15)").css({
        "top": Himg / 35,
        "left": wMap / 1.33
    });
    //end  image15

    // image16
    $(".vitri img:nth-child(16)").css({
        "top": Himg / 2.85,
        "left": wMap / 1.322
    });
    //end  image16

    // image17
    $(".vitri img:nth-child(17)").css({
        "top": Himg / 2.25,
        "left": wMap / 1.295
    });
    //end  image17

    // image18
    $(".vitri img:nth-child(18)").css({
        "top": Himg / 2.88,
        "left": wMap / 1.27
    });
    //end  image18

    // image19
    $(".vitri img:nth-child(19)").css({
        "top": Himg / 1.74,
        "left": wMap / 1.24
    });
    //end  image19

    // image20
    $(".vitri img:nth-child(20)").css({
        "top": Himg / 1.19,
        "left": wMap / 1.153
    });
    //end  image20

    // image21
    $(".vitri img:nth-child(21)").css({
        "top": Himg / 4.2,
        "left": wMap / 1.12
    });
    //end  image21

    // image22
    $(".vitri img:nth-child(22)").css({
        "top": Himg / 6.7,
        "left": wMap / 1.03
    });
    //end  image22

    // image23
    $(".vitri img:nth-child(23)").css({
        "top": Himg / 1.9,
        "left": wMap / 4.3
    });
    //end  image23

    // image24
    $(".vitri img:nth-child(24)").css({
        "top": Himg / 2.57,
        "left": wMap / 2.39
    });
    //end  image24

    // image25
    $(".vitri img:nth-child(25)").css({
        "top": Himg / 2.4,
        "left": wMap / 1.67
    });
    //end  image25
    $(window).resize(function () { location.reload(); });
});

$(document).on('ready', function () {
    $(".center").slick({
        dots: false,
        slidesToShow: 5,
        slidesToScroll: 1,
        arrows: false,
        focusOnSelect: true,
        swipe: false,
    });
    
    $('.left').click(function () {
        $('.center').slick('slickPrev');
    })

    $('.right').click(function () {
        $('.center').slick('slickNext');
    })

});