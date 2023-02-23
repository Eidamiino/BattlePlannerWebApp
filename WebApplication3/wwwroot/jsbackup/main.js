 fetch('/Home/Data')
  .then(response => response.json())
  .then(data => console.log(data))