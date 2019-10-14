using System;

namespace Com.Github.Knose1.Common.Twinning.Curve
{
	public abstract class Curve
	{
		protected float minX = 0;
		protected float maxX = 1;
		protected float minY = float.NaN;
		protected float maxY = float.NaN;

		protected Curve()
		{
			if (float.IsNaN(minY)) minY = GetCurve(minX);
			if (float.IsNaN(maxY)) maxY = GetCurve(maxX);
		}

		abstract protected float GetCurve(float x);

		public float In(float lerpX)
		{
			if (lerpX < 0) lerpX = 0;
			else if (lerpX > 1) lerpX = 1;

			float curveY = GetCurve((maxX - minX) * lerpX + minX);

			return (curveY-minY) / (maxY - minY);
		}

		public float Out(float lerpX)
		{
			if (lerpX < 0) lerpX = 0;
			else if (lerpX > 1) lerpX = 1;

			float curveY = GetCurve((minX - maxX) * lerpX + maxX);

			return (curveY - maxY) / (minY - maxY);
		}

		public float InOut(float lerpX)
		{
			if (lerpX < 0) lerpX = 0;
			else if (lerpX > 1) lerpX = 1;

			if (lerpX < 0.5) return In(lerpX * 2) / 2;
			else return Out(lerpX * 2 - 1) / 2 + 0.5f;
		}


		public float OutIn(float lerpX)
		{
			if (lerpX < 0) lerpX = 0;
			else if (lerpX > 1) lerpX = 1;

			if (lerpX < 0.5) return Out(lerpX * 2) / 2;
			else return In(lerpX * 2 - 1) / 2 + 0.5f;
		}
	}
}

//To test this class on http://proglab.fr/?lang=js :
/*
var canvasSize = 300;

var canvas = document.body.appendChild( document.createElement( "canvas" ) );
var context = canvas.getContext( "2d" );
canvas.width = canvasSize; // Largeur en pixels
canvas.height = canvasSize; // hauteur en pixels
canvas.style.transform = "rotateX(180deg)";

var minX = -Math.PI / 3;
var maxX = Math.PI / 2;

var minY = NaN;
var maxY = NaN;

if (isNaN(minY)) minY = GetCurve(minX);
if (isNaN(maxY)) maxY = GetCurve(maxX);

function GetCurve(x) {
	return Math.sin(x - Math.PI / 2;);
}

function In(lerpX)
{
	if (lerpX < 0) lerpX = 0;
	else if (lerpX > 1) lerpX = 1;

	var curveY = GetCurve((maxX - minX) * lerpX + minX);

	return (curveY-minY) / (maxY - minY);
}

function Out(lerpX)
{
	if (lerpX < 0) lerpX = 0;
	else if (lerpX > 1) lerpX = 1;

	var curveY = GetCurve((minX - maxX) * lerpX + maxX);

	return (curveY - maxY) / (minY - maxY);
}

function InOut(lerpX)
{
	if (lerpX < 0) lerpX = 0;
	else if (lerpX > 1) lerpX = 1;

	if (lerpX < 0.5) return In(lerpX * 2) / 2;
	else return Out(lerpX * 2 - 1) / 2 + 0.5;
}

var pas = 0.01;
var curveSize = 100;
var startPosition = (canvasSize - curveSize) / 2;
for (var i = 0; i <= 1; i += pas) {
	context.beginPath();
	context.arc(i * curveSize + startPosition, InOut(i) * curveSize + startPosition, 3, 0, 2*Math.PI);
	context.fill();
	
}

*/
