$(document).ready(function() {
  $(".room-seats__seat").click(function() {
    $(this).addClass("choosen");
    $(this).css('background-image', 'url("../../image/seat_choosen.svg")');
    let colChoosen = $(this).find(".room-seats__seat-number").text();
    let rowChoosen = $(this).parent().find(".room-seats__row-number").text();
  });
});