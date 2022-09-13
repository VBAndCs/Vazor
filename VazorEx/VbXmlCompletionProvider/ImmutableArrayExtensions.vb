Option Explicit Off
Option Infer On
Option Strict Off
' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Collections.Immutable
Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Runtime.CompilerServices

''' <summary>
''' The collection of extension methods for the <see cref= "ImmutableArray(OfT)"/> type
''' </summary>
Friend Module ImmutableArrayExtensions
    ''' <summary>
    ''' Converts a sequence to an immutable array.
    ''' </summary>
    ''' <typeparam name="T">Elemental type of the sequence.</typeparam>
    ''' <param name="items">The sequence to convert.</param>
    ''' <returns>An immutable copy of the contents of the sequence.</returns>
    ''' <exception cref= "ArgumentNullException">If items is null (default)</exception>
    ''' <remarks>If the sequence is null, this will throw <see cref= "ArgumentNullException"/></remarks>
    <Extension()>
    Public Function AsImmutable(Of T)(items As IEnumerable(Of T)) As ImmutableArray(Of T)
        Return ImmutableArray.CreateRange(Of T)(items)
    End Function

    ''' <summary>
    ''' Converts a sequence to an immutable array.
    ''' </summary>
    ''' <typeparam name="T">Elemental type of the sequence.</typeparam>
    ''' <param name="items">The sequence to convert.</param>
    ''' <returns>An immutable copy of the contents of the sequence.</returns>
    ''' <remarks>If the sequence is null, this will return an empty array.</remarks>
    <Extension()>
    Public Function AsImmutableOrEmpty(Of T)(items As IEnumerable(Of T)) As ImmutableArray(Of T)
        If items Is Nothing Then
            Return ImmutableArray(Of T).Empty
        End If

        Return ImmutableArray.CreateRange(Of T)(items)
    End Function

    ''' <summary>
    ''' Converts a sequence to an immutable array.
    ''' </summary>
    ''' <typeparam name="T">Elemental type of the sequence.</typeparam>
    ''' <param name="items">The sequence to convert.</param>
    ''' <returns>An immutable copy of the contents of the sequence.</returns>
    ''' <remarks>If the sequence is null, this will return the default (null) array.</remarks>
    <Extension()>
    Public Function AsImmutableOrNull(Of T)(items As IEnumerable(Of T)) As ImmutableArray(Of T)
        If items Is Nothing Then
            Return CType(Nothing, ImmutableArray(Of T))
        End If

        Return ImmutableArray.CreateRange(Of T)(items)
    End Function

    ''' <summary>
    ''' Converts an array to an immutable array. The array must not be null.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="items">The sequence to convert</param>
    ''' <returns></returns>
    <Extension()>
    Public Function AsImmutable(Of T)(items As T()) As ImmutableArray(Of T)
        Debug.Assert(items IsNot Nothing)
        Return ImmutableArray.Create(Of T)(items)
    End Function

    ''' <summary>
    ''' Converts a array to an immutable array.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="items">The sequence to convert</param>
    ''' <returns></returns>
    ''' <remarks>If the sequence is null, this will return the default (null) array.</remarks>
    <Extension()>
    Public Function AsImmutableOrNull(Of T)(items As T()) As ImmutableArray(Of T)
        If items Is Nothing Then
            Return CType(Nothing, ImmutableArray(Of T))
        End If

        Return ImmutableArray.Create(Of T)(items)
    End Function

    ''' <summary>
    ''' Converts an array to an immutable array.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="items">The sequence to convert</param>
    ''' <returns>If the array is null, this will return an empty immutable array.</returns>
    <Extension()>
    Public Function AsImmutableOrEmpty(Of T)(items As T()) As ImmutableArray(Of T)
        If items Is Nothing Then
            Return ImmutableArray(Of T).Empty
        End If

        Return ImmutableArray.Create(Of T)(items)
    End Function

    ''' <summary>
    ''' Reads bytes from specified <see cref= "MemoryStream"/>.
    ''' </summary>
    ''' <param name="stream">The stream.</param>
    ''' <returns>Read-only content of the stream.</returns>
    <Extension()>
    Public Function ToImmutable(stream As MemoryStream) As ImmutableArray(Of Byte)
        Return ImmutableArray.Create(Of Byte)(stream.ToArray())
    End Function

    ''' <summary>
    ''' Maps an immutable array to another immutable array.
    ''' </summary>
    ''' <typeparam name="TItem"></typeparam>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="items">The array to map</param>
    ''' <param name="map">The mapping delegate</param>
    ''' <returns>If the items's length is 0, this will return an empty immutable array</returns>
    <Extension()>
    Public Function SelectAsArray(Of TItem, TResult)(items As ImmutableArray(Of TItem), map As Func(Of TItem, TResult)) As ImmutableArray(Of TResult)
        Return ImmutableArray.CreateRange(items, map)
    End Function

    ''' <summary>
    ''' Maps an immutable array to another immutable array.
    ''' </summary>
    ''' <typeparam name="TItem"></typeparam>
    ''' <typeparam name="TArg"></typeparam>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="items">The sequence to map</param>
    ''' <param name="map">The mapping delegate</param>
    ''' <param name="arg">The extra input used by mapping delegate</param>
    ''' <returns>If the items's length is 0, this will return an empty immutable array.</returns>
    <Extension()>
    Public Function SelectAsArray(Of TItem, TArg, TResult)(items As ImmutableArray(Of TItem), map As Func(Of TItem, TArg, TResult), arg As TArg) As ImmutableArray(Of TResult)
        Return ImmutableArray.CreateRange(items, map, arg)
    End Function


    ''' <summary>
    ''' Returns an empty array if the input array is null (default)
    ''' </summary>
    <Extension()>
    Public Function NullToEmpty(Of T)(array As ImmutableArray(Of T)) As ImmutableArray(Of T)
        Return If(array.IsDefault, ImmutableArray(Of T).Empty, array)
    End Function



    <Extension()>
    Friend Function Concat(Of T)(first As ImmutableArray(Of T), second As ImmutableArray(Of T)) As ImmutableArray(Of T)
        Return first.AddRange(second)
    End Function

    <Extension()>
    Friend Function Concat(Of T)(first As ImmutableArray(Of T), second As T) As ImmutableArray(Of T)
        Return first.Add(second)
    End Function

    <Extension()>
    Friend Function HasDuplicates(Of T)(array As ImmutableArray(Of T), comparer As IEqualityComparer(Of T)) As Boolean
        Select Case array.Length
            Case 0, 1
                Return False

            Case 2
                Return comparer.Equals(array(0), array(1))
            Case Else
                Dim [set] As System.Collections.Generic.HashSet(Of T) = New HashSet(Of T)(comparer)
                For Each i As T In array
                    If Not [set].Add(i) Then
                        Return True
                    End If
                Next

                Return False
        End Select
    End Function

    <Extension()>
    Public Function Count(Of T)(items As ImmutableArray(Of T), predicate As Func(Of T, Boolean)) As Integer
        If items.IsEmpty Then
            Return 0
        End If

        Dim _count As Integer = 0
        For i As Integer = 0 To items.Length - 1
            If predicate(items(i)) Then
                _count += 1
            End If
        Next

        Return _count
    End Function

    Private Structure ImmutableArrayProxy(Of T)
        Friend MutableArray As T()
    End Structure

End Module
