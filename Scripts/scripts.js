$(document).ready(function () {
    $(".slideanim").each(function () {
        var pos = $(this).offset().top;
        var winTop = $(window).scrollTop();
        if (pos < winTop + 750) {
            $(this).addClass("slide-bottom");
        }
    });

    $(window).scroll(function () {
        $(".slideanim").each(function () {
            var pos = $(this).offset().top;
            var winTop = $(window).scrollTop();
            if (pos < winTop + 750) {
                $(this).addClass("slide-bottom");
            }
        });
    });
});

$(document).ready(function () {
    $(".slideanim-top").each(function () {
        var pos = $(this).offset().top;
        var winTop = $(window).scrollTop();
        if (pos < winTop + 750) {
            $(this).addClass("slide-top");
        }
    });

    $(window).scroll(function () {
        $(".slideanim-top").each(function () {
            var pos = $(this).offset().top;
            var winTop = $(window).scrollTop();
            if (pos < winTop + 750) {
                $(this).addClass("slide-top");
            }
        });
    });
});

$(document).ready(function () {
    $(".slideanim-right").each(function () {
        var pos = $(this).offset().top;
        var winTop = $(window).scrollTop();
        if (pos < winTop + 750) {
            $(this).addClass("slide-right");
        }
    });

    $(window).scroll(function () {
        $(".slideanim-right").each(function () {
            var pos = $(this).offset().top;
            var winTop = $(window).scrollTop();
            if (pos < winTop + 750) {
                $(this).addClass("slide-right");
            }
        });
    });
});

$(document).ready(function () {
    $(".slideanim-left").each(function () {
        var pos = $(this).offset().top;
        var winTop = $(window).scrollTop();
        if (pos < winTop + 750) {
            $(this).addClass("slide-left");
        }
    });

    $(window).scroll(function () {
        $(".slideanim-left").each(function () {
            var pos = $(this).offset().top;
            var winTop = $(window).scrollTop();
            if (pos < winTop + 750) {
                $(this).addClass("slide-left");
            }
        });
    });
});

// Event listeners for +/- buttons (adult)
document.querySelector('.btn-default.adult-plus').addEventListener('click', function () {
    adultInput.value = parseInt(adultInput.value) + 1;
    updateTotalPrice();
});

document.querySelector('.btn-default.adult-minus').addEventListener('click', function () {
    if (parseInt(adultInput.value) > 1) {
        adultInput.value = parseInt(adultInput.value) - 1;
        updateTotalPrice();
    }
});

// Event listeners for +/- buttons (child)
document.querySelector('.btn-default.child-plus').addEventListener('click', function () {
    childInput.value = parseInt(childInput.value) + 1;
    updateTotalPrice();
});

document.querySelector('.btn-default.child-minus').addEventListener('click', function () {
    if (parseInt(childInput.value) > 0) {
        childInput.value = parseInt(childInput.value) - 1;
        updateTotalPrice();
    }
});

function showTab(event, tabName) {
    var tabContents = document.getElementsByClassName("tabcontent");
    for (var i = 0; i < tabContents.length; i++) {
        tabContents[i].style.opacity = 0;
        tabContents[i].style.display = "none";
    }
    var tabButtons = document.getElementsByClassName("tablinks");
    for (var i = 0; i < tabButtons.length; i++) {
        tabButtons[i].className = tabButtons[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    setTimeout(function () {
        document.getElementById(tabName).style.opacity = 1;
    }, 10);
    event.currentTarget.className += " active";
}
showTab(event, 'bao-gom');
