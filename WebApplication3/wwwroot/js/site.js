// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//fetch('/data', { method: 'POST' })
//	.then(response => response.json())
//	.then(data => console.log(data))
function onBtnCreateRequirement() {
	var btn = document.getElementById("saveButton");
	var text = document.getElementById("requirementName");
	btn.addEventListener("click", function () {
		console.log(text.value)
		console.log("Nade mnou hroch")
		createRequirement(text.value)
	})
}

function renderRequirements() {
	var select = document.getElementById("choices");

	let requirements = $('#requirements').html();
	Mustache.parse(requirements);



	fetch('/api/Requirements', { method: 'GET' })
		.then(response => response.json())
		.then(data => data.forEach(requirement => {
			let option = document.createElement("option")
			option.value = requirement.name;
			option.text = requirement.name;
			select.append(option)

			let renderRequirements = Mustache.render(requirements, requirement)
			console.log(renderRequirements)
			$("#requirementList").append(renderRequirements)
		}))

}



function renderResources() {
	let resources = $('#resources').html();
	Mustache.parse(resources);

	fetch('/api/Resource', { method: 'GET' })
		.then(response => response.json())
		.then(data => data.forEach(resource => {

			let renderResources = Mustache.render(resources, resource)
			console.log(renderResources)
			$("#resourceList").append(renderResources)
		}))

}

$(() => {
	renderRequirements()
	renderResources()
})
//on render