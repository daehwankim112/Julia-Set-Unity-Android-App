Shader "Explorer/Mandelbrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		//We want to program zomming. Variable _Area hold the area we are going to render.
		//(0,0 "Center of the mandelbrot", 4, 4 "Size of area we are going to rendar.")
		_Area("Area", vector) = (0, 0, 4, 4)
		_R("Red", range(0,1)) = 0.5
		_G("Green", range(0, 1)) = 0.5
		_B("Blue", range(0, 1)) = 0.5
		[Toggle] _TimePass("Time", Float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float4 _Area;
            sampler2D _MainTex;
			float _R, _G, _B, _TimePass;
			uniform float color_diff = 0;

            fixed4 frag (v2f i) : SV_Target
            {
				//Fundamental Mendelbrot loop.
				float2 c = float2(-0.512511498387847167, 0.521295573094847167);
				float2 z = _Area.xy + (i.uv - .5)*_Area.zw;
				float iter;
				float maxiter = 1000;
				float r = 20;
				float r2 = r * r;
				float4 col;

				for (iter = 0; iter < maxiter; iter++) {
					z = float2(z.x*z.x - z.y*z.y, 2 * z.x*z.y) + c;
					if (length(z) > r) break;
				}

				if (iter == maxiter)
					return 0;

				float dist = length(z); //Distance from origin
				float fracIter = (dist - r) / (r2 - r);
				fracIter = log2( log(dist) / log(r) ); //double exponential interpolation

				iter -= fracIter;

                float m = sqrt(iter/ maxiter);
				col = (cos(float4(_R, _G, _B, 1)*m * 20 + color_diff)*0.5 + .5);

				return col;
            }
            ENDCG
        }
    }
}
