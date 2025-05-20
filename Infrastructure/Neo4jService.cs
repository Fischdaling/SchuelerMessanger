using Neo4j.Driver;

namespace SchuelerChatBackendProject.Controllers;

public class Neo4jService : IAsyncDisposable
{
	private readonly IDriver _driver;

	public Neo4jService(IConfiguration config)
	{
		_driver = GraphDatabase.Driver(
			"bolt://localhost:7687",
			AuthTokens.Basic("neo4j", "schueler"));
	}

	public async Task LogRelationshipAsync(string sender, string receiver)
	{
		var session = _driver.AsyncSession();
		try
		{
			await session.RunAsync(@"
                MERGE (a:User {id: $sender})
                MERGE (b:User {id: $receiver})
                MERGE (a)-[r:MESSAGED]->(b)
                ON CREATE SET r.count = 1
                ON MATCH SET r.count = r.count + 1
            ", new { sender, receiver });
		}
		finally
		{
			await session.CloseAsync();
		}
	}
	
	public async Task<int?> GetShortestFriendPathLengthAsync(string userA, string userB)
	{
		var session = _driver.AsyncSession();
		try
		{
			var result = await session.RunAsync(@"
            MATCH (a:User {id: $userA}), (b:User {id: $userB}),
                  p = shortestPath((a)-[:FRIEND*]-(b))
            RETURN length(p) AS hops
        ", new { userA, userB });

			var record = await result.SingleAsync();
			return record?["hops"].As<int?>();
		}
		finally
		{
			await session.CloseAsync();
		}
	}



	public async ValueTask DisposeAsync() => await _driver.DisposeAsync();
}
