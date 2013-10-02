singleCoffeeFile = () ->
    element = document.getElementById("single-coffee-file")
    element.innerText = element.innerText.replace(" not ", " ")
    element.className = element.className + " pass"