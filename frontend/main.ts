import {Student} from "./model/Student";
import {GetAllStudents} from "./Services/ServiceStudent";

function populateDropdown(selectId: string, students: Student[]) {
    const select = document.getElementById(selectId) as HTMLSelectElement;
    select.innerHTML = ''; // Clear previous options

    students.forEach(student => {
        const option = document.createElement('option');
        option.value = student.Vorname + ' ' + student.Nachname;
        option.textContent = student.Vorname + ' ' + student.Nachname;
        select.appendChild(option);
    });
}

document.addEventListener('DOMContentLoaded', async () => {
    try {
        const users = await GetAllStudents();

        populateDropdown('user', users);
        populateDropdown('person', users);

        const userSelect = document.getElementById('user') as HTMLSelectElement;
        const personSelect = document.getElementById('person') as HTMLSelectElement;

        userSelect.addEventListener('change', () => {
            const selectedUser = userSelect.value;
            for (var i = 0; i < personSelect.options.length; i++){
                personSelect.options[i].disabled = personSelect.options[i].value === selectedUser;
            }
        });
    } catch (error) {
        console.error('Error loading users:', error);
    }
});



