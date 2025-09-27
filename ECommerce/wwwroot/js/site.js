window.onload = function () {
    document.addEventListener("click", documentActions);
    function documentActions(e) {


        const targetElement = e.target;



        if (targetElement.classList.contains('header-main__burger')) {
            document.body.classList.toggle('_lock');
            document.querySelector('.header-main__burger').classList.toggle("_active");
            document.querySelector('.header-main__menu').classList.toggle('_active');

        }


        if (window.innerWidth < 767 && document.querySelector('.header')) { //&& isMobile.touchScreen()
            let link = document.querySelectorAll('.flipping-button');
            for (i = 0; i < link.length; i++) {
                let subMenu = link[i].nextElementSibling;
                let thisArrow = link[i].parentNode.parentNode.parentNode.parentNode.parentNode;

                link[i].addEventListener('click', function () {
                    subMenu.classList.toggle('_display');
                    thisArrow.classList.toggle('_swipe');
                })
            }
            let arrow = document.querySelectorAll('.goback__link');
            for (i = 0; i < arrow.length; i++) {
                let subMenu2 = arrow[i].parentNode.parentNode.parentNode.parentNode.parentNode;
                let thisArrow2 = arrow[i].parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;

                arrow[i].addEventListener('click', function () {
                    setTimeout(function () {
                        subMenu2.classList.remove('_display');
                    }, 300);
                    thisArrow2.classList.remove('_swipe');
                })
            }
        }

    }
}


if (window.innerWidth < 767 && document.querySelector('.info__title')) {
    var acc = document.getElementsByClassName("info__title");
    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {
            if (this.classList.contains('_active')) {
                this.classList.remove('_active');
                this.nextElementSibling.style.maxHeight = null;
            } else {
                // remove this to not autoclose/
                for (i = 0; i < acc.length; i++) {
                    acc[i].classList.remove('_active');
                    acc[i].nextElementSibling.style.maxHeight = null;
                }
                this.classList.add('_active')
                this.nextElementSibling.style.maxHeight = this.nextElementSibling.scrollHeight + "px";
            }
        });
    }
}







if (document.querySelector('.recomend-slider__slider')) {
    //===add needed css properties for some slasses to work corectly
    document.querySelector(".recomend-slider__slider").children[0].style.display = "flex";
    var el = document.querySelector(".recomend-slider__slider").children[0]
    for (var i = 0; i < el.children.length; i++) {
        el.children[i].style.flexShrink = "0";
    }
    var catalogSlider = new Swiper('.recomend-slider__slider', {
        //
        wrapperClass: "recomend-slider__wrapper",
        slideClass: "recomend-slider__slide",
        navigation: {
            nextEl: '.slider-recomend-header__button-next',
            prevEl: '.slider-recomend-header__button-prev',
        },
        observer: true,
        observeParents: true,
        //
        // direction: 'vertical', // direction
        //if direction 'vertical' set flex-direction column + set max height to slider-catalog__slider
        spaceBetween: 20,
        slidesPerView: "auto",
        // 
        watchOveFflow: false,
        speed: 300, //speed of slide turning
        preloadImages: false, // disable preload of img
        grabCursor: true,
        //
        breakpoints: {
            // 767: {
            //    spaceBetween: 20,
            //    slidesPerView: "auto",
            // },
            992: {
                slidesPerView: 4,
                spaceBetween: 40,
                freeMode: false,
            },
        },
        loop: false, // infinite
        freeMode: {
            enabled: true,
            minimumVelocity: 0.02,
            momentum: true,
            momentumRatio: 0.55,
            momentumVelocityRatio: 0.75,
            momentumBounce: true,
            momentumBounceRatio: 2,
            // sticky: true, // may change 
        },

    });
}


const popup = document.querySelector('.Popup');
const declineButton = popup?.querySelector('.Popup__DeclineButton');
const approveButton = popup?.querySelector('.Popup__ApproveButton');

if (popup && declineButton && approveButton) {
    function openDeletePopup(deleteUrl) {
        approveButton.href = deleteUrl;
        popup.classList.add('_active');
        document.body.classList.add('_lock');
    }

    declineButton.addEventListener('click', (event) => {
        event.preventDefault();
        popup.classList.remove('_active');
        document.body.classList.remove('_lock');
    });

    popup.addEventListener('click', (event) => {
        if (event.target === popup) {
            popup.classList.remove('_active');
            document.body.classList.remove('_lock');
        }
    });
}



// cart: right side menu ==================================================================
const cart = document.querySelector('.cart');
if (cart && cart.querySelector('.cart__body')) {
    document.addEventListener('click', (event) => {
        if (event.target.closest('.cart__icon')) {
            event.preventDefault();
            cart.classList.toggle('_active');
        } else if (event.target.closest('.cart__close') || !event.target.closest('.cart')) {
            cart.classList.remove('_active');
        }
    });
}
