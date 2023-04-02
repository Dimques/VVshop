document.addEventListener("DOMContentLoaded", function () {
  // Бургер
  let burger = document.querySelector('.js-burger');
  let menu = document.querySelector('.js-menu');

  burger.addEventListener('click', function () {
    burger.classList.toggle('active');
    menu.classList.toggle('active');
  });

  // Слайдеры
  new Swiper(".js-popular", {
    slidesPerView: 2,
    spaceBetween: 20,
    centeredSlides: true,
    loop: true,
    autoplay: {
      delay: 5000,
    },
    pagination: {
      el: ".pagination__wrapper",
      clickable: true,
    },
    navigation: {
      prevEl: '.pagination__arrow_prev',
      nextEl: '.pagination__arrow_next',
    },

    breakpoints: {
      // when window width is >= 320px
      // when window width is >= 480px
      768: {
        slidesPerView: 4,
        spaceBetween: 30
      },
      // when window width is >= 640px
      1200: {
        slidesPerView: 5,
        spaceBetween: 40
      }
    }
  });

  new Swiper(".js-catalog-slider", {
    slidesPerGroup: 3,
    slidesPerView: 3,
    grid: {
      rows: 3,
      fill: "row",
    },
    spaceBetween: 40,
    speed: 1,
    simulateTouch: false,
    pagination: {
      el: ".catalog-pagination__dots",
      clickable: true,
      renderBullet: function (index, className) {
        return '<div class="' + className + '">' + (index + 1) + "</div>";
      },
    },
    navigation: {
      prevEl: '.catalog-pagination_prev',
      nextEl: '.catalog-pagination_next',
    },
  });

  // Слайдеры в Товар подробно
  let detailThumbs = new Swiper(".js-detail-slider-thumbs", {
    loop: true,
    spaceBetween: 41,
    slidesPerView: 1,
    watchSlidesProgress: true,
    grid: {
      rows: 2,
    },
  });
  let detailMain = new Swiper(".js-detail-slider-main", {
    loop: true,
    spaceBetween: 41,
    navigation: {
      nextEl: ".pagination__arrow_next",
      prevEl: ".pagination__arrow_prev",
    },
    pagination: {
      el: ".pagination__wrapper",
      clickable: true,
    },
    thumbs: {
      swiper: detailThumbs,
    },
  });

  // Модалка фильтр
  let modalFilters = document.querySelector('.js-filters');
  let modalFiltersBtn = document.querySelector('.js-filters-btn');
  let modalFiltersClose = document.querySelector('.js-filters-close');
  let modalFiltersOverlay = document.querySelector('.js-filters-overlay');
  let body = document.querySelector('body');

  if (modalFilters) {
    modalFiltersBtn.addEventListener('click', function () {
      modalFilters.classList.add('active');
      body.classList.add('no-scroll');
    });

    modalFiltersClose.addEventListener('click', function () {
      modalFilters.classList.remove('active');
      body.classList.remove('no-scroll');
    });

    modalFiltersOverlay.addEventListener('click', function () {
      modalFilters.classList.remove('active');
      body.classList.remove('no-scroll');
    });
  };

  // Карточка, добавление в избранное
  let card = document.querySelectorAll('.card');

  if (card.length) {
    card.forEach(card => {
      let cardBtnToFav = card.querySelector('.card__tofav');

      cardBtnToFav.addEventListener('click', function () {
        card.classList.toggle('js-added-tofav');
      });
    });
  };

  // Продукт детальная, добавление в избранное
  let prodDetToFav = document.querySelector('.detail__add-to-fav');

  if (prodDetToFav) {
    prodDetToFav.addEventListener('click', function () {
      this.closest('.detail').classList.toggle('added-to-fav');
    });
  };

  // Выпадающие списки
  let dropdownAll = document.querySelectorAll('.js-dropdown');

  if (dropdownAll.length) {
    dropdownAll.forEach(dropdown => {
      let mainChackbox = dropdown.querySelector('.js-main-chackbox');
      let title = dropdown.querySelector('.js-dropdown-title');
      let checkboxAll = dropdown.querySelector('.js-ckeckbox-all');
      let options = dropdown.querySelectorAll('.js-option');

      // Клик по чекбоксу
      mainChackbox.addEventListener('click', function () {
        if (mainChackbox.checked) {
          checkboxAll.checked = true;
        } else {
          checkboxAll.checked = false;
          options.forEach(option => {
            if (option.querySelector('input').checked) {
              option.querySelector('input').checked = false;
            };
          });
        };
      });

      // Клик по заголовку
      title.addEventListener('click', function () {
        dropdown.classList.toggle('active');
        if (!mainChackbox.checked && dropdown.classList.contains('active')) {
          mainChackbox.checked = true;
          checkboxAll.checked = true;
        };
      });

      // Клик по кнопке выбрать все
      checkboxAll.addEventListener('click', function () {
        options.forEach(option => {
          if (option.querySelector('input').checked) {
            option.querySelector('input').checked = false;
          };
        });
      });

      // Клик по пунктам меню
      options.forEach(option => {
        option.addEventListener('click', function () {
          checkboxAll.checked = false;
          mainChackbox.checked = true;
        });
      });
    });
  };
});