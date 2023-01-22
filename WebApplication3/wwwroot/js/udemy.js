'use strict'

let correctAnswer = Math.trunc(Math.random() * 20)

let guess = document.querySelector(".guess")
let check = document.querySelector(".check")
const message = document.querySelector(".message")
let again = document.querySelector(".again")

check.addEventListener("click", function () {
    let input = guess.value
    if (input == correctAnswer) message.textContent = "Correct Answer"
    else if (input > correctAnswer) message.textContent = "Too high"
    else message.textContent = "Too low"
})

again.addEventListener("click", function () {
    correctAnswer = Math.trunc(Math.random() * 20)
    message.textContent = "Start guessing"
})


