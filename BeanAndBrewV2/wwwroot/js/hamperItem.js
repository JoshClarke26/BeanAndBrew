function addItem(event) {
    var itemId = event.target.id;
    var amount = document.getElementById("S-" + itemId).value
    var path = "https://" + window.location.host + "/order/hamper/item/" + amount + "/" + itemId;
    console.log(path)
    return window.location.replace(path)
}