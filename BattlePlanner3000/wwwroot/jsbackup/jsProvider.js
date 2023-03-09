// async function createRequirement(name) {
//     let response = await fetch('/api/Requirements', { method: 'POST', body: JSON.stringify({ name }) })
//     console.log(response)
//     return await response.json()
// }

// function createRequirement(requirementName) {
//     let response = fetch("http://localhost:5266/api/Requirements", {
//         headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
//         method: 'POST',
//         body: JSON.stringify(requirementName)
//     })
//         .then(res => res.json())
//         .then(res => console.log(res));
//     return response.json
// }

