function saveFormData() {
    let formData = {};
    let budgetInput = document.querySelector('input[type="number"][name="budget"]');
    /*const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    const numberInputs = document.querySelectorAll('input[type="number"]');
    const checkedValues = [];

    for (let i = 0; i < checkboxes.length; i++) {
        let element = checkboxes[i]
        if (element.checked) {
            switch (element.name) {
                case "gender": checkedValues.push({
                    name: element.name === "female" ? "ForWomen" : "ForMan",
                    value: element.value,
                });
                break;
                case "budget": checkedValues.push({
                    name: element.name,
                    value: element.value
                });
                break;
                case ""

            }
            
            checkedValues.push({
                name: checkboxes[i].name,
                value: checkboxes[i].value
            });
        }
    }

    for (let i = 0; i < numberInputs.length; i++) {
        if (numberInputs[i].value) {
            checkedValues.push({
                name: numberInputs[i].name,
                value: numberInputs[i].value
            });
        }
    }

    const formData = {
        name: document.getElementById('name').value,
        email: document.getElementById('email').value,
        checkedValues: checkedValues
    };
*/
    fetch('/outfits/construct', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Response from server:', data);
            
            localStorage.setItem('checkedValues', data);
        })
        .catch(error => {
            console.error('Error sending data to server:', error);
        });
}

/*_expectedSerializedParams = "{\"Colors\":[5,7]," +
    "\"ClothKinds\":[0,1,2,3,4]," +
    "\"Budget\":5000," +
    "\"ForWomen\":true," +
    "\"ForMan\":true," +
    "\"TopSize\":3," +
    "\"BottomSize\":3,\"" +
    "ShoeSize\":40}";*/
        