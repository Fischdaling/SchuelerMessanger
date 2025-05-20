import {Student} from "../model/Student";


export async function GetAllStudents() : Promise<Student[]>  {
    const headers: Headers = new Headers();
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");
    headers.set("X-Custom-Headers", "CustomValue");

    const request : RequestInfo = new Request("http://localhost:5202/api/students", {
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


    async function fetchShortestPath(userA: string, userB: string): Promise<number | null> {
        const res = await fetch(`/api/users/shortest-path?from=${userA}&to=${userB}`);
        if (!res.ok) {
            throw new Error(`Failed to fetch shortest path`);
        }
        const data = await res.json();
        return data.hops ?? null;
    }
}