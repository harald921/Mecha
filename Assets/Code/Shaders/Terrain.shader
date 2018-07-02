Shader "Custom/Terrain" 
{
	Properties 
	{
		_MainTex ("Tile Texture", 2DArray) = "white" {}
		_IndexTex("Shine (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.5
		
		UNITY_DECLARE_TEX2DARRAY(_MainTex);

		struct Input 
		{
			float2 uv2_IndexTex : TEXCOORD1;

			float3 worldPos;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			float4 color = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(IN.worldPos.xz * 0.25, IN.uv2_IndexTex.x));
			
			o.Albedo	 = color.rgb;
		}
		ENDCG
	}
}