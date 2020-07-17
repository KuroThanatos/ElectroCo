// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.toast').toast("show");

$('.dropdown-submenu a.dropright').on("mouseover", function (e) {
    $(".toggled").toggle();
    $(".toggled").removeClass("toggled");
    $(this).next('ul').toggle();
    $(this).next('ul').toggleClass("toggled");
    e.stopPropagation();
    e.preventDefault();
});




