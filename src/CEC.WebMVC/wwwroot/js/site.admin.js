window.onscroll = function () { onScroll() };

var header = document.getElementById("adminHeader");
var sticky = header.offsetTop;

function onScroll() {
    if (window.pageYOffset > sticky) {
        header.classList.add("position-sticky");
    } else {
        header.classList.remove("position-sticky");
    }
}