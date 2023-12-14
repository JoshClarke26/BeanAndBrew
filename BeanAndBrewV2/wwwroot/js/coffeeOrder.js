function orderCoffee(event) {
    var coffeeId = event.target.id;
    window.location.replace("https://" + window.location.host + "/order/coffee/" + coffeeId)
}