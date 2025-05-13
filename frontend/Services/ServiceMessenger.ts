import {Student} from "./Student.cs";

export async function GetMessageByStudentId(id: string) : Promise<String>  {
    const headers: Headers = new Headers();
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");
    headers.set("X-Custom-Headers", "CustomValue");

    const request : RequestInfo = new Request(`"http://localhost:5202/api/students/${id}"`, {
        method: "GET",
        headers: headers,
        body: JSON.stringify({id: id})
    });

    try {
        const response = await fetch(request);

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data as string;
    } catch (error) {
        console.error("Error fetching messages:", error);
        throw error;
    }

}