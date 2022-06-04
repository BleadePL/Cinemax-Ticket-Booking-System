$(document).ready(function() {
  var colChoosen;
  var rowChoosen;
  var showId;
  var isPurchased;

  function SetCookie(c_name,value,expiredays) {
		const exdate=new Date()
		exdate.setDate(exdate.getDate()+expiredays)
		document.cookie=c_name+ "=" + value +
		((expiredays==null) ? "" : ";expires="+exdate.toGMTString())
	};

  $(".room-seats__seat").click(function() {
    $(this).addClass("choosen");
    $(this).css('background-image', 'url("../../image/seat_choosen.svg")');
    colChoosen = $(this).find(".room-seats__seat-number").text();
    rowChoosen = $(this).parent().find(".room-seats__row-number").text();
    showId = $(".room-seats__showId").text();
    isPurchased = true;

    if($(this).find(".seat-reservation-hidden-value").text() === "-1") {
      isPurchased = false;
    };
  });

  $(".testAction").click(function() {
    SetCookie("Reservation", `[{showId:${showId}, row:${rowChoosen}, col:${colChoosen}, isPurchased:${isPurchased}}]`, 1);
  });
});