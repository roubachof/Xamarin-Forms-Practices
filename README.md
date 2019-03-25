# Xamarin-Forms-Practices

Test app for the https://github.com/roubachof/Sharpnado.Presentation.Forms components.

<p align="center">
  <img src="__Docs__/will_ferrel.png" width="300"  />
</p>

| Platform | Build Status                                                                                                                             |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| Android  | [![Build status](https://build.appcenter.ms/v0.1/apps/23f44cf3-7656-4932-9d82-f654db6afc82/branches/master/badge)](https://appcenter.ms) |
| iOS      | [![Build status](https://build.appcenter.ms/v0.1/apps/ddd14409-1f42-4521-ae8d-6f9891de2714/branches/master/badge)](https://appcenter.ms) |


If you want to learn how to use the ```Sharpnado.Presentation.Forms``` components such as:

```HorizontalListView``` for Xamarin Forms:
  * Snapping on first or middle element
  * Padding and item spacing
  * Handles (```NotifyCollectionChangedAction(``` Add, Remove and Reset actions
  * View recycling

<p align="center">
  <img src="__Docs__/horizontal_snap_center.gif" width="250"  />
</p>

```Grid``` collection view (```HorizontalListView``` with ```ListLayout``` set to ```Grid```):
  * Drag and Drop
  * Padding and item spacing
  * Handles (```NotifyCollectionChangedAction(``` Add, Remove and Reset actions
  * View recycling

<p align="center">
  <img src="__Docs__/drag_and_drop.gif" width="250"  />
</p>

```TaskLoaderView``` displays an ```ActivityLoader``` while loading then:
  * Handles error with custom messages and icons
  * Handles empty states
  * Don't show activity loader for refresh scenarios (if data is already shown)
  * Handles retry with button
  * Pure Xamarin Forms view: no renderers

<p align="center">
  <img src="__Docs__/task_loader_view.gif" width="250"  />
</p>

"Pure" ```Xamarin.Forms``` Tabs

<table>
	<thead>
		<tr>
			<th>Top tabs</th>
			<th>Bottom bar tabs</th>
      <th>Custom tabs</th>
		</tr>
	</thead>
	<tbody>
		<tr>
      <td><img src="__Docs__/top_tabs.gif" width="250" /></td>
  		<td><img src="__Docs__/bottom_tabs.gif" width="250" /></td>
			<td><img src="__Docs__/spam_tabs.gif" width="250" /></td>
		</tr>
    <tr>
      <td>```UnderlinedTabItem```</td>
			<td>```BottomTabItem```</td>
      <td>inherit from ```TabItem```</td>
    </tr>
  </tbody>
</table>


you came to the right place :)

The components documentation can be found here: https://github.com/roubachof/Sharpnado.Presentation.Forms.
