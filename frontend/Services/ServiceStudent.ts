import {Student} from "../model/Student";


export async function GetAllStudents() : Promise<Student[]> {
    const headers: Headers = new Headers();
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");
    headers.set("X-Custom-Headers", "CustomValue");

    const request: RequestInfo = new Request("http://localhost:5202/api/students", {
        method: "GET",
        headers: headers
    });

    try {
        const response = await fetch(request);

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data as Student[];
    } catch (error) {
        console.error("Error fetching students:", error);
        throw error;
    }

}
export async function GetStudentById(userA: Student): Promise<Student> {
    const headers: Headers = new Headers();
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");
    headers.set("X-Custom-Headers", "CustomValue");

    const request : RequestInfo = new Request(`http://localhost:5202/api/students/${userA.Id}`, {
        method: "GET",
        headers: headers
    });

    try {
        const response = await fetch(request);

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data as Student;
    } catch (error) {
        console.error("Error fetching students:", error);
        throw error;
    }
}

export async function SendMessage(MessageText: string, SenderId: string, ReceiverId: string) : Promise<void> {
    const headers: Headers = new Headers();
    headers.append("Content-Type", "application/json");
    headers.append("Accept", "application/json");

    const request: RequestInfo = new Request(`http://localhost:5202/api/students`, {
        method: "POST",
        headers: headers,
        body: JSON.stringify({MessageText, SenderId, ReceiverId})
    });

    return await fetch(request)
        .then(res => {
            console.log("got response:", res);
        });
}

export async function fetchShortestPath(userA: string, userB: string): Promise<number | null> {
    const headers: Headers = new Headers();
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");
    headers.set("X-Custom-Headers", "CustomValue");

    const request : RequestInfo = new Request(`http://localhost:5202/api/students/${userA}`, {
        method: "GET",
        headers: headers,
        body: JSON.stringify({userA, userB})
    });
    try {
        const response = await fetch(request);
        if (!response.ok) {
            throw new Error(`Failed to fetch shortest path`);
        }
        const data = await response.json();
        return data.hops ?? null;
    } catch (error) {
        console.error("Error fetching shortest path:", error);
        throw error;
    }
}



