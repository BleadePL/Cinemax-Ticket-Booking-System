$(document).ready(function() {

  $('.room-seats__row').children('.room-seats__seat').each(function (){
    if(parseInt($(this).find('.seat-reservation-hidden-value').text()) !== -1){
      $(this).css('background-image', 'url("../../image/seat_reserved.svg")')
    }
  });

  var reservatedSeats = [];

  function SetCookie(c_name,value,expiredays) {
      var cookieValue = encodeURIComponent(value);
      const exdate = new Date()
		exdate.setDate(exdate.getDate()+expiredays)
      document.cookie = c_name + "=" + cookieValue +
          ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString() + ";path =/;domain=localhost")
	};

  let location= $('#checkout').attr("href");
  $('#checkout').removeAttr('href');

  $(".room-seats__seat").click(function() {

    var reservation = {
      showId: 0,
      col: 0,
      row: 0,
      isPurchased: false
    };

    $(this).addClass("choosen");
    $(this).css('background-image', 'url("../../image/seat_choosen.svg")');
    reservation.col = $(this).find(".room-seats__seat-number").text();
    reservation.row = $(this).parent().find(".room-seats__row-number").text();
    reservation.showId = $(".room-seats__showId").text();
    reservation.isPurchased = false;

    if($(this).find(".seat-reservation-hidden-value").text() !== "-1") {
      reservation.isPurchased = true;
    };

    reservatedSeats.push(reservation);

    if(reservatedSeats.length === 1){
      $('#checkout').attr("href", location);
    };
  });

  $("#checkout").click(function() {
    SetCookie("Reservation", JSON.stringify(reservatedSeats), 1);
    reservatedSeats = [];
  });
});

$(document).ready(function() {
  let getRoomId = parseInt($('.PatternID-hidden-value').text());

  if(getRoomId === 1) {
    $('#room-pattern-two').remove();
    $('#room-pattern-three').remove();
    $('#room-pattern-one').css('display', 'block');
  } else if(getRoomId === 2) {
    $('#room-pattern-one');
    $('#room-pattern-three');
    $('#room-pattern-two').css('display', 'block');
  } else if(getRoomId === 3) {
    $('#room-pattern-one').remove();
    $('#room-pattern-two').remove();
    $('#room-pattern-three').css('display', 'block');
  } else {
    $('#room-pattern-one').css('display', 'block');
  };
});