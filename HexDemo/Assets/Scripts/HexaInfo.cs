using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexaInfo
{
    public const float outerRadius = 10f;
    // Pythagorean Theorem.
    public const float innerRadius = outerRadius* 0.866025404f;

	public static Vector2[] corners = {
		new Vector2(0f, outerRadius),
		new Vector2(innerRadius,0.5f * outerRadius ),
		new Vector2(innerRadius,-0.5f * outerRadius),
		new Vector2(0f, -outerRadius),
		new Vector2(-innerRadius, -0.5f * outerRadius),
		new Vector2(-innerRadius, 0.5f * outerRadius),
		new Vector2(0f,outerRadius)

	};


}

    
    

